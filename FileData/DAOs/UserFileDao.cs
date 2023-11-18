using Application.DaoInterfaces;
using Domain_A1.DTOs;
using Domain_A1.Models;
using FileData.FileDaoImplem;

namespace FileData.DAOs;


public class UserFileDao : IUserDaoA1
{
    private readonly FileContextA1 _contextA1;

    public UserFileDao(FileContextA1 contextA1)
    {
        this._contextA1 = contextA1;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (_contextA1.Users.Any())
        {
            userId = _contextA1.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        _contextA1.Users.Add(user);
        _contextA1.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = _contextA1.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = _contextA1.Users.FirstOrDefault(u =>
            u.Id == id
        );
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = _contextA1.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = _contextA1.Users.Where(u => u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }
    
    
 
}

   


