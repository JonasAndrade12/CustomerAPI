namespace CustomerAPI.Services;

using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

public class CustomerService : ICustomerService
{
    private readonly Dictionary<Guid, Customer> _customers = new();
    private readonly DBContext db;

    public CustomerService(IDbContextFactory<DBContext> _dbContext)
    {
        this.db = _dbContext.CreateDbContext();
    }

    public void Create(Customer? customer)
    {
        if (customer is null)
        {
            return;
        }

        db.Customers.Add(customer);
        db.SaveChanges();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await db.Customers.Where(x => x.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await db.Customers.ToListAsync();
    }

    public async void UpdateAsync(Customer customer)
    {
        var existingCustomer = await GetByIdAsync(customer.Id);
        if (existingCustomer is null)
        {
            return;
        }

        // TODO: Create extension
        existingCustomer.FirstName = customer.FirstName;
        existingCustomer.Surname = customer.Surname;
        existingCustomer.Email = customer.Email;
        existingCustomer.Password = customer.Password;

        db.SaveChanges();
    }

    public async void DeleteAsync(Guid id)
    {
        var existingCustomer = await GetByIdAsync(id);
        if (existingCustomer is null)
        {
            return;
        }

        db.Customers.Remove(existingCustomer);
        db.SaveChanges();
    }
}