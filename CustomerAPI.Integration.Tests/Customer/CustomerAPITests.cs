namespace CustomerAPI.Integration.Tests.Customer;

using CustomerAPI.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public class CustomerAPITests
{

    [Fact]
    public async Task GetAll_HaveValues_ReturnValues()
    {
        // Arrange 
        const string Name = "Jonas";

        await using var application = new CustomerAPIApplicationTests();

        var client = application.CreateClient();

        await client.PostAsJsonAsync("/v1/customers", new Customer { FirstName = Name, Surname = "Andrade", Email = "jonas@andrade.pt", Password = "1234" });

        // Act
        var result = await client.GetFromJsonAsync<List<Customer>>("/v1/customers");

        // Assert
        result.Should().ContainSingle();
        result.Should().NotBeNullOrEmpty();
        result[0].FirstName.Should().Be(Name);
    }

    [Fact]
    public async Task GetAll_HaveValues_ReturnEmptyValues()
    {
        // Arrange 
        await using var application = new CustomerAPIApplicationTests();

        var client = application.CreateClient();

        // Act
        var result = await client.GetFromJsonAsync<List<Customer>>("/v1/customers");

        // Assert
        result.Should().BeEmpty();
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetById_HaveValues_ReturnValue()
    {
        // Arrange 
        const string Name = "Jonas";
        const string Email = "jonas@andrade.pt";

        await using var application = new CustomerAPIApplicationTests();

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/v1/customers", new Customer { FirstName = Name, Surname = "Andrade", Email = Email, Password = "1234" });

        // Act
        var result = await client.GetFromJsonAsync<Customer>(response.Headers.Location);

        // Assert
        result.FirstName.Should().Be(Name);
        result.Email.Should().Be(Email);
    }

    [Fact]
    public async Task Post_CreateCustomer_ReturnSucess()
    {
        // Arrange 
        const string Name = "Jonas";
        const string Email = "jonas@andrade.pt";

        await using var application = new CustomerAPIApplicationTests();

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/v1/customers", new Customer { FirstName = Name, Surname = "Andrade", Email = Email, Password = "1234" });

        // Act
        var result = await client.PostAsJsonAsync("/v1/customers", new Customer { FirstName = Name, Surname = "Andrade", Email = Email, Password = "1234" });

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var customer = await client.GetFromJsonAsync<Customer>(result.Headers.Location);
        customer.Should().NotBeNull();
        customer.Email.Should().Be(Email);
    }

    [Fact]
    public async Task Put_CreateCustomer_ReturnSucess()
    {
        // Arrange 
        const string Name = "Jonas";
        const string Email = "jonas@andrade.pt";
        const string NewEmail = "jonasandrade@andrade.pt";

        await using var application = new CustomerAPIApplicationTests();

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/v1/customers",
            new Customer
            {
                FirstName = Name,
                Surname = "Andrade",
                Email = Email,
                Password = "1234"
            });

        // Act
        var result = await client.PutAsJsonAsync(response.Headers.Location,
            new Customer
            {
                FirstName = Name,
                Surname = "Andrade",
                Email = NewEmail,
                Password = "1234",
                Id = new Guid(response.Headers.Location.ToString().Split("/").Last())
            });

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var customer = await client.GetFromJsonAsync<Customer>(response.Headers.Location);
        customer.Should().NotBeNull();
        customer.Email.Should().Be(NewEmail);
    }

    [Fact]
    public async Task Delete_CustomerRemoved_ReturnSucess()
    {
        // Arrange 
        const string Name = "Jonas";
        const string Email = "jonas@andrade.pt";

        await using var application = new CustomerAPIApplicationTests();

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/v1/customers", new Customer { FirstName = Name, Surname = "Andrade", Email = Email, Password = "1234" });

        // Act
        var result = await client.DeleteAsync(response.Headers.Location);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var customer = await client.GetAsync(response.Headers.Location);
        customer.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
