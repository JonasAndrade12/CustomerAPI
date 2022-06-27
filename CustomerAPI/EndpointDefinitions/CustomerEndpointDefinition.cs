namespace CustomerAPI.EndpointDefinitions;

using CustomerAPI.Models;
using CustomerAPI.Services;

public class CustomerEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/v1/customers", GetAllCustomersAsync);
        app.MapGet("/v1/customers/{id}", GetCustomerByIdAsync);
        app.MapPost("/v1/customers", CreateCustomer);
        app.MapPut("/v1/customers/{id}", UpdateCustomerAsync);
        app.MapDelete("/v1/customers/{id}", DeleteCustomerById);
    }

    internal async static Task<List<Customer>> GetAllCustomersAsync(ICustomerService service)
    {
        return await service.GetAllAsync();
    }

    internal async Task<IResult> GetCustomerByIdAsync(ICustomerService service, Guid id)
    {
        var customer = await service.GetByIdAsync(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal IResult CreateCustomer(ICustomerService service, Customer customer)
    {
        service.Create(customer);
        return Results.Created($"/v1/customers/{customer.Id}", customer);
    }

    internal async Task<IResult> UpdateCustomerAsync(ICustomerService service, Guid id, Customer updatedCustomer)
    {
        var customer = await service.GetByIdAsync(id);
        if (customer is null)
        {
            return Results.NotFound();
        }

        service.UpdateAsync(updatedCustomer);
        return Results.Ok(updatedCustomer);
    }

    internal IResult DeleteCustomerById(ICustomerService service, Guid id)
    {
        service.DeleteAsync(id);
        return Results.Ok();
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<ICustomerService, CustomerService>();
    }
}