using Application.DaoInterfaces;
using Domain_A1.DTOs;
using Domain_A1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class PostEfcDao: IPostDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        try
        {
          
            await context.SaveChangesAsync();
        }
        catch (Exception  e)
        {
            Console.Write(e);
        }

        return added.Entity;
     
    }

    public Task<IEnumerable<Post>> GetAsync()
    {
        IQueryable<Post> query = context.Posts.AsQueryable();
        return query.ToListAsync().ContinueWith(task => task.Result.AsEnumerable());
    }

    public Task<IEnumerable<Post>> GetOnePostAsync(GetSpecificPostByTitleDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Where(post => post.Title.ToLower() == searchParameters.Title.ToLower());
        return query.ToListAsync().ContinueWith(task => task.Result.AsEnumerable());
    }
}