namespace CustomerAPI;

using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options) => Database.EnsureCreated();

    public DbSet<Customer> Customers { get; set; }

}
