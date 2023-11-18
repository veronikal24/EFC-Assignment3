using Domain_A1.DTOs;
using Domain_A1.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto todo);
    Task<IEnumerable<Post>> GetAsync();
    Task<IEnumerable<Post>> GetOnePostAsync(GetSpecificPostByTitleDto searchParameters);
}