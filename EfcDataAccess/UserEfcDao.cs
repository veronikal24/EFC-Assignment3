using Application.DaoInterfaces;
using Domain_A1.DTOs;
using Domain_A1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class UserEfcDao : IUserDaoA1


{
    
    private readonly PostContext context;

    public UserEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        try
        {
            await context.SaveChangesAsync();
        }

        catch(Exception e)
        {
            Console.WriteLine(e);
        }

        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        // Filtering by UserName using case-insensitive comparison
        IQueryable<User> query = context.Users
            .Where(u => u.UserName.ToLower().Contains(searchParameters.UsernameContains.ToLower()));

        // Execute the query asynchronously and retrieve the results as a list of users
        List<User> users = await query.ToListAsync();

        return users;
    }

    public Task<User?> GetByIdAsync(int id)
    {
        IQueryable<User> query = context.Users.Where(u => u.Id.Equals(id));
        return query.FirstOrDefaultAsync();
 
    }
}