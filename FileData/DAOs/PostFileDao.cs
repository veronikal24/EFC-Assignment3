using Application.DaoInterfaces;
using Domain_A1.DTOs;
using Domain_A1.Models;
using FileData.FileDaoImplem;

namespace FileData.DAOs;

public class PostFileDao: IPostDao
{
    private readonly FileContextA1 context;

    public PostFileDao(FileContextA1 context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.UserId);
            id++;
        }
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync()
    {
        IEnumerable<Post> result =context.Posts.AsEnumerable();
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Post>> GetOnePostAsync(GetSpecificPostByTitleDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.AsQueryable();

        if (!string.IsNullOrEmpty(searchParameters.Title))
        {
            query = query.Where(post =>
                post != null && post.Title != null && post.Title.Equals(searchParameters.Title, StringComparison.OrdinalIgnoreCase));

        }

        IEnumerable<Post> results = query.ToList();
        return Task.FromResult(results);
        
    }
}