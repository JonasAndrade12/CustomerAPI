namespace CustomerAPI.Models;

public class Customer
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string FirstName { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}