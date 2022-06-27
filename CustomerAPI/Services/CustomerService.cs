namespace CustomerAPI.Services;

using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

public class CustomerService : ICustomerService
{
    private readonly Dictionary<Guid, Customer> _customers = new();
    private DBContext db;

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
    }

    public Customer? GetById(Guid id)
    {
        return _customers.GetValueOrDefault(id);
    }

    public List<Customer> GetAll()
    {
        return db.Customers.ToList();
    }

    public void Update(Customer customer)
    {
        var existingCustomer = GetById(customer.Id);
        if (existingCustomer is null)
        {
            return;
        }

        _customers[customer.Id] = customer;
    }

    public void Delete(Guid id)
    {
        _customers.Remove(id);
    }
}