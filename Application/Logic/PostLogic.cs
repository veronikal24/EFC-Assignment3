using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain_A1.DTOs;
using Domain_A1.Models;

namespace Application.Logic;

public class PostLogic: IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDaoA1 userDao;

    public PostLogic(IPostDao postDao, IUserDaoA1 userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }
    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.UserId} was not found.");
        }

        ValidateTodo(dto);
        Post todo = new Post();
        todo.UserId = user.Id;
        todo.Body = dto.Body;
        todo.Title = dto.Title;
       
        
        Post created = await postDao.CreateAsync(todo);
        return created;
    }

   

    private void ValidateTodo(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
    public Task<IEnumerable<Post>> GetAsync()
    {
        return postDao.GetAsync();
    }

    public Task<IEnumerable<Post>> GetOnePostAsync(GetSpecificPostByTitleDto searchParameters)
    {
        return postDao.GetOnePostAsync(searchParameters);
    }
}
    
   
