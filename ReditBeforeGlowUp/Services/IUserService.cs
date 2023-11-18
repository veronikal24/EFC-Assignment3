using Domain_A1.DTOs;
using Domain_A1.Models;

namespace ReditBeforeGlowUp.Services.Http;

public interface IUserService
{
    Task CreateAsync(UserCreationDto dto);
  
    Task<ICollection<Domain_A1.Models.User>> GetUserByNameAsync(string ? title);
}