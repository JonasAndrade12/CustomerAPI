namespace CustomerAPI.Unit.Tests.Controllers;

using AutoFixture;
using CustomerAPI.EndpointDefinitions;
using CustomerAPI.Models;
using CustomerAPI.Services;
using FluentAssertions;
using Moq;
using System.Net.Http.Json;
using Xunit;

public class CustomerControllerTests
{
    readonly Fixture fixture;
    readonly Mock<ICustomerService> customerServiceMock;
    readonly CustomerEndpointDefinition controller;

    public CustomerControllerTests()
    {
        this.fixture = new Fixture();
        this.customerServiceMock = new Mock<ICustomerService>();
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
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAll_HaveValues_ReturnValues()
    {
        // Arrange 
        await using var application = new CustomerAPIApplicationTests();

        var client = application.CreateClient();

        var customers = new List<Customer>
        {
            this.fixture.Create<Customer>()
        };

        customerServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(customers);

        // Act
        var result = await client.GetFromJsonAsync<List<Customer>>("/v1/customers");

        // Assert
        result.Should().HaveCount(1);
        // customerServiceMock.VerifyAll();
    }
}

