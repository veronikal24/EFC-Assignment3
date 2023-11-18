using System.ComponentModel.DataAnnotations;
using FileData.DAOs;
using FileData.FileDaoImplem;
using SharedFolder.Models;

using WebAPI.Services;

namespace WebApi.Services;

public class AuthService : IAuthService
{

    private readonly IList<User> users = new List<User>
    
    {
        new User
        {
            Domain = "via",
            Name = "Veronika Lietavcova",
            Password = "VeronikaISTesting",
            Role = "Teacher",
            Username = "VeronikaISTesting",
            SecurityLevel = 4,
            Email = "Vli@via.dk",
            Age = 23,
            
        },
        new User
        {
            Domain = "via",
            Name = "Veronika Lietavcova",
            Password = "dsasdfasdfasdfdsdf",
            Role = "Teacher",
            Username = "SPECIALPACKAGE",
            SecurityLevel = 4,
            Email = "Vli@via.dk",
            Age = 23,
            
        },
        
     
    };
    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        Task<Domain_A1.Models.User> existingUser2 = CheckTheAPi(username, password);
        if (existingUser == null && existingUser2 == null)
        {
            throw new Exception("User not found");
        }

        if (existingUser2 != null)
        {
            SharedFolder.Models.User user2 = new User();
            user2.Username = username;
            user2.Password = password;
            user2.Domain = "via";
            user2.Name = username;
            user2.Role = "Student";
            user2.Email = username + "via.dk";
            user2.SecurityLevel = 4;
            user2.Age = 24;
           
            RegisterUser(user2);
            if (!user2.Password.Equals(password) )
            {
                throw new Exception("Password mismatch");
            }
            return Task.FromResult(user2);
        }

        if (!existingUser.Password.Equals(password) )
        {
            throw new Exception("Password mismatch");
        }
        

        return Task.FromResult(existingUser);
    }

    public  Task<Domain_A1.Models.User> CheckTheAPi(string username, string password)
    {
      
        FileContextA1 fileContextA1 = new FileContextA1();
        UserFileDao fileDao = new UserFileDao(fileContextA1);
        Task<Domain_A1.Models.User?>  user = fileDao.GetByUsernameAsync(username);
       
        if (user == null)
        {
            throw new Exception("User not found");
        }

       
        return user;
    }


    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
  
        
        users.Add(user);
        
        return Task.CompletedTask;
    }
}