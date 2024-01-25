using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using TheFullStackTeam.Application.Countries.Commands;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Tests.Countries.Commands
{
    public class CreateCountryHandlerTests
    {

        [Fact]
        public void WhenCallingConstructor_ItShouldCreateAnInstance()
        {
            // ARRANGE: 
            var options = new DbContextOptionsBuilder<TheFullStackTeamDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContextMock = new Mock<TheFullStackTeamDbContext>(options);

            // ACT:
            var objectToTest = new CreateCountryHandler(dbContextMock.Object);

            // ASSERT:
            Assert.NotNull(objectToTest);
        }


        [Fact]
        public async Task HandleShouldAddAndSaveCountryAynchronously()
        {
            // ARRANGE:
            var countryModel = new CountryModel { NativeName = "CountryTest" };
            var command = new CreateCountryCommand(countryModel);
            var cancellationToken = new CancellationToken();

            // Prepare mock for our test.
            var options = new DbContextOptionsBuilder<TheFullStackTeamDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContextMock = new Mock<TheFullStackTeamDbContext>(options);

            // Setup Add Async
            dbContextMock
                .Setup(dcm => dcm.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()))
                .Callback<Country, CancellationToken>((c, token) => { })
                .Returns((Country countryModel, CancellationToken token) =>
                {                   
                    return new ValueTask<EntityEntry<Country>>();
                })
                .Verifiable();

            // Setup SaveChangesAsync
            dbContextMock
                .Setup(d => d.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1)
                .Verifiable();

            var objectToTest = new CreateCountryHandler(dbContextMock.Object);

            // ACT:
            var createdCountry = await objectToTest.Handle(command, cancellationToken);

            // ASSERT:
            // Verify AddAsync was called
            dbContextMock.Verify(x => x.AddAsync<Country>(It.IsAny<Country>(), cancellationToken), Times.Once);
            // Verify SaveChangesAsync was called
            dbContextMock.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
        }
    }
}
