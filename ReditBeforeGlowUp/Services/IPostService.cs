using Domain_A1.DTOs;
using Domain_A1.Models;

namespace ReditBeforeGlowUp.Services.Http.Implementations;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAllAsync();
    Task<ICollection<Post>> GetByTitleAsync(string ? title);
    
}