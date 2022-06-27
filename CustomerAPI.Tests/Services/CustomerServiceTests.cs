namespace CustomerAPI.Tests.Services
{
    using AutoFixture;
    using CustomerAPI.Models;
    using CustomerAPI.Services;
    using Xunit;

    public class CustomerServiceTests
    {
        Fixture fixture;
        CustomerService customerService;
        // Moq<DBContext>

        public CustomerServiceTests()
        {
            this.fixture = new Fixture();
            //this.customerService = new CustomerService();
        }

        [Fact]
        public void GetAll_HaveValues_ReturnValues()
        {
            // Arrange 
            var customers = new List<Customer>
            {
                this.fixture.Create<Customer>()
            };

            // Act
            // var result = this.customerService.GetAll();

            // Assert
            // result.Should();
        }
    }
}
