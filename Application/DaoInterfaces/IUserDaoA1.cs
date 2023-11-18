using Domain_A1.DTOs;
using Domain_A1.Models;

namespace Application.DaoInterfaces;

public interface IUserDaoA1
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    Task<User?> GetByIdAsync(int id);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
  
}