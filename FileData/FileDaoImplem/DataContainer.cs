using System.Text.Json;
using Domain_A1.Models;

namespace FileData.FileDaoImplem;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Post> Posts { get; set; }
    
    
   
}
