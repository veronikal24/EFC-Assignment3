using System.Text.Json.Serialization;

namespace Domain_A1.Models;


public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public ICollection<Post> Posts { get; set; }
    

    
    
    
}