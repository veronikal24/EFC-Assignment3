namespace Domain_A1.DTOs;

public class SearchUserParametersDto
{
    public string? UsernameContains { get;  }

    public SearchUserParametersDto(string? usernameContains)
    {
        UsernameContains = usernameContains;
    }
}