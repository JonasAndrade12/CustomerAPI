namespace CustomerAPI.Services;

using CustomerAPI.Models;

public interface ICustomerService
{
    void Create(Customer customer);

    Task<Customer?> GetByIdAsync(Guid id);

    Task<List<Customer>> GetAllAsync();

    void UpdateAsync(Customer customer);

    void DeleteAsync(Guid id);
}