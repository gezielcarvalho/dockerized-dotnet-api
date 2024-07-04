namespace CloudCustomers.API.Models;

public class User(
    string name,
    string email,
    string phone,
    string address,
    string city,
    string state,
    string zip,
    string country,
    string password,
    string confirmPassword)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Phone { get; set; } = phone;
    public string Address { get; set; } = address;
    public string City { get; set; } = city;
    public string State { get; set; } = state;
    public string Zip { get; set; } = zip;
    public string Country { get; set; } = country;
    public string Password { get; set; } = password;
    public string ConfirmPassword { get; set; } = confirmPassword;
}