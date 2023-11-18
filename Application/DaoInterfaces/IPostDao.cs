using Domain_A1.DTOs;
using Domain_A1.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAsync();
    Task<IEnumerable<Post>> GetOnePostAsync(GetSpecificPostByTitleDto searchParameters);
}