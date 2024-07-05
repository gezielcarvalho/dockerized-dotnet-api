using CloudCustomers.API.Models;
using System.Collections.Generic;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetUsers() =>
        [
            new User
            {
                Id = 1,
                Name = "Jane Doe",
                Username = "jane",
                Email = "jane@doe.com",
                Address = new Address
                {
                    Street = "123 Main St",
                    Suite = "Apt. 1",
                    City = "Any-town",
                    Zipcode = "12345",
                    Geo = new Geo
                    {
                        Lat = "40.7128",
                        Lng = "74.0060"
                    }
                },
                Phone = "123-456-7890",
                Website = "jane-doe.com",
                Company = new Company
                {
                    Name = "Jane Doe Inc.",
                    CatchPhrase = "Empowering Women",
                    Bs = "Harness real-time e-markets"
                }
            },

            new User
            {
                Id = 2,
                Name = "John Doe",
                Username = "john",
                Email = "john@doe.com",
                Address = new Address
                {
                    Street = "123 Main St",
                    Suite = "Apt. 2",
                    City = "Any-town",
                    Zipcode = "12345",
                    Geo = new Geo
                    {
                        Lat = "40.7128",
                        Lng = "74.0060"
                    }
                },
                Phone = "123-456-7890",
                Website = "john-doe.com",
                Company = new Company
                {
                    Name = "John Doe Inc.",
                    CatchPhrase = "Empowering Men",
                    Bs = "Harness real-time e-markets"
                }
            }
        ];
    }
}
