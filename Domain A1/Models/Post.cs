using System.ComponentModel.DataAnnotations;

namespace Domain_A1.Models;

public class Post
{
     // assumption I am making here is that each post can be 
     // only done by a registered user and you cannot see posts without having an account
     // this is usually like it is on Reddit
    
     [Key]
     public int PostId { get; set; }
     public int UserId { get; set; }
     public string Body { get; set; }
     [MaxLength(150)]
    public string Title { get; set; }

    public User User { get; set; }
    public  Post(){}
}