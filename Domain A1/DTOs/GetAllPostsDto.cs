namespace Domain_A1.DTOs;

public class GetAllPostsDto
{
    
    public int? UserId { get;}

    public string? Title { get;}

    public GetAllPostsDto(int? userid, string? title)
    {
        UserId = userid;
        Title = title;
    }
}