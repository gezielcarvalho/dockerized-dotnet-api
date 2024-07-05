using System.Net;
using CloudCustomers.API.Models;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Newtonsoft.Json;

namespace CloudCustomers.API.Services;

public interface IUsersService
{
    public Task<List<User>?> GetUsers();
}
public class UsersService(HttpClient httpClient) : IUsersService
{
    public async Task<List<User>?> GetUsers()
    {
        var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
        var content = await response.Content.ReadAsStringAsync();
        return response.StatusCode == HttpStatusCode.NotFound ? null : JsonConvert.DeserializeObject<List<User>>(content);
    }
}

