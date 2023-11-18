using Domain_A1.DTOs;
using Domain_A1.Models;

namespace WebAPI.Services;

public interface IPostService
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAllPostsAsynch();
}