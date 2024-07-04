using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services;

public interface IUsersService
{
    public Task<List<User>> GetUsers();
}
public class UsersService: IUsersService
{
    public async Task<List<User>>GetUsers()
    {
        var list = new List<User>
        {
            new User(
                "John Doe",
                "john@doe.com",
                "123-456-7890",
                "123 Main St",
                "Anytown",
                "NY",
                "12345",
                "USA",
                "password",
                "password")
        };
        await Task.Delay(1000);
        return list;
    }
}

