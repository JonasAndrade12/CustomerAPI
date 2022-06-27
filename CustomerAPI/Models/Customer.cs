namespace CustomerAPI.Models;

public class Customer
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string FirstName { get; init; } = default!;

    public string Surname { get; init; } = default!;

    public string Email { get; init; } = default!;

    public string Password { get; init; } = default!;
}