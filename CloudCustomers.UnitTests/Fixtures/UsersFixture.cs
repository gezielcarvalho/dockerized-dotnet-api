using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UsersFixture
{
    public static List<User> GetUsers() =>
    [
        new User(
            "Jane Doe",
            "jane@doe.com",
            "123-456-7890",
            "123 Main St",
            "Any-town",
            "NY",
            "12345",
            "USA",
            "password",
            "password"),
        new User(
            "John Doe",
            "john@doe.com",
            "123-456-7890",
            "123 Main St",
            "Any-town",
            "NY",
            "12345",
            "USA",
            "password",
            "password"),
    ];
}