using System.Net;
using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CloudCustomers.API.Services;

public interface IUsersService
{
    public Task<List<User>?> GetUsers();
}
public class UsersService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig) : IUsersService
{
    public async Task<List<User>?> GetUsers()
    {
        var response = await httpClient.GetAsync("");
        var content = await response.Content.ReadAsStringAsync();
        return response.StatusCode == HttpStatusCode.NotFound ? null : JsonConvert.DeserializeObject<List<User>>(content);
    }
}

