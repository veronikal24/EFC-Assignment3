

using Application.Logic;
using Application.LogicInterfaces;
using Domain_A1.Models;
using Microsoft.AspNetCore.Mvc;
using Domain_A1.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : Controller
{
    private readonly IPostLogic _postLogic;

    public PostController(IPostLogic postLogic)
    {
        this._postLogic= postLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody]PostCreationDto dto)
    {
        try
        {
            Post created = await _postLogic.CreateAsync(dto);
            return Created($"/post/{created.Title}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
   
    
    [HttpGet("GetPostByTitle"), AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Post>>> GetOnePost([FromQuery] string? title)
    {
        try
        {
            GetSpecificPostByTitleDto parameters = new(title);
            var todos = await _postLogic.GetOnePostAsync(parameters);
            return Ok(todos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("GetAllPosts"), AllowAnonymous]
    public async Task<IEnumerable<Post>> GetAllPosts()
    {

        return await _postLogic.GetAsync();
    }


}