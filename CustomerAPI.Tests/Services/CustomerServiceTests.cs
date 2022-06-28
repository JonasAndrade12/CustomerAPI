namespace CustomerAPI.Unit.Tests.Services
{
    using AutoFixture;
    using CustomerAPI.Models;
    using CustomerAPI.Services;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CustomerServiceTests
    {
        readonly Fixture fixture;
        readonly CustomerService customerService;
        readonly Mock<DBContext> dbContextMoq;
        readonly Mock<IDbContextFactory<DBContext>> factoryMoq;
        readonly Mock<DbSet<Customer>> dbSetMoq;

        public CustomerServiceTests()
        {
            this.dbContextMoq = new Mock<DBContext>();
            this.dbSetMoq = new Mock<DbSet<Customer>>();
            this.factoryMoq = new Mock<IDbContextFactory<DBContext>>();
            this.fixture = new Fixture();

            // dbContextMoq.Setup(m => m.Customers).Returns(dbSetMoq.Object);


            // Usar DB em memoria

            this.customerService = new CustomerService(this.factoryMoq.Object);
        }

        [Fact]
        public void GetAll_HaveValues_ReturnValues()
        {
            // Arrange 
            var customers = new List<Customer>
            {
                this.fixture.Create<Customer>()
            };

            dbSetMoq.Setup(x => x.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(customers);

            // Act
            var result = this.customerService.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
        }
    }
}
