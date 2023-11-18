using System.Text;
using Domain_A1.Models;

namespace ReditBeforeGlowUp.Services.Http.Implementations;


using System.Text.Json;
using Domain_A1.DTOs;
using SharedFolder.Models;

public class PostHttpClient : IPostService
{
    private readonly HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

  

    public  async Task  CreateAsync(PostCreationDto dto)
    {
        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/Post", dto);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Post post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
            
                // Optionally, you can return the created post or perform further actions.
            }
            else
            {
                // Handle the case when the HTTP request was not successful
                // You might want to log the response content or specific error details
                string errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
                Console.WriteLine($"Error content: {errorContent}");

                // You can throw a custom exception or handle the error as needed
                throw new Exception($"HTTP request failed with status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle network-related errors
            Console.WriteLine($"HTTP request error: {ex.Message}");
            throw new Exception("Network error occurred during the HTTP request.", ex);
        }
        catch (Exception ex)
        {
            // Handle other exceptions that may occur during the HTTP request
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
    

    public async Task<ICollection<Post>> GetAllAsync()
    {
        HttpResponseMessage response = await client.GetAsync("Post/GetAllPosts");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> todos = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return todos;
    }

    public async Task<ICollection<Post>> GetByTitleAsync(string Title)
    {
  
        Title = Title.Replace(" ", "%20");
        HttpResponseMessage response = await client.GetAsync("/Post/GetPostByTitle?title="+Title);
       
        if (response.IsSuccessStatusCode)
        {
            // The request was successful (HTTP status code 200-299).
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the response content if it represents JSON data.
            ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true }); 
            return posts;
        }
        else
        {
            throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
        }

        
    }



}

