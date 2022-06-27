using CustomerAPI;
using CustomerAPI.EndpointDefinitions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<DBContext>
    (options => options.UseMySql(
        "Server=mysql;Port=33060;Database=customers;Uid=root;Pwd=passwor",
        ServerVersion.Parse("8.0.29-mysql")));

builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));

var app = builder.Build();
app.UseEndpointDefinitions();

app.UseHttpsRedirection();

app.Run();
