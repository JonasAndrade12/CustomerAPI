using CustomerAPI;
using CustomerAPI.EndpointDefinitions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<DBContext>
    (options => options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.Parse("8.0.29")));

builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));

var app = builder.Build();
app.UseEndpointDefinitions();

app.UseHttpsRedirection();

app.MapGet("/hello", async (DBContext contexto) =>
{
   await contexto.Customers.ToListAsync();
});

app.Run();
