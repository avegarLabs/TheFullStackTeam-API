using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using TheFullStackTeam.Application.Services;
using TheFullStackTeam.Domain.Entities.Base;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Apllication.Services.Tests
{
    public class MonikerServiceTests
    {
        //[Fact]
        //public async Task FindValidMoniker_WhenCalledWithValidSuggestedMoniker_ReturnsValidMoniker()
        //{
        //    // Arrange
        //    var expectedMoniker = "test-moniker-1";
        //    var suggestedMoniker = "Test Moniker";
        //    var nickNamedEntityMock = new Mock<NicknamedEntity>();
        //    var dbContextMock = new Mock<TheFullStackTeamDbContext>();
        //    var dbSetMock = new Mock<DbSet<NicknamedEntity>>();
        //    dbSetMock.Setup(m => m.CountAsync(It.IsAny<Expression<Func<NicknamedEntity, bool>>>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(0);
        //    dbContextMock.Setup(m => m.Set<NicknamedEntity>()).Returns(dbSetMock.Object);
        //    var monikerService = new MonikerService(dbContextMock.Object);

        //    // Act
        //    var actualMoniker = await monikerService.FindValidMoniker<NicknamedEntity>(suggestedMoniker);

        //    // Assert
        //    Assert.Equal(expectedMoniker, actualMoniker);
        //}

        //[Fact]
        //public async Task FindValidMoniker_WhenCalledWithExistingMoniker_ReturnsValidMonikerWithIncrementedCount()
        //{
        //    // Arrange
        //    var expectedMoniker = "test-moniker-2";
        //    var suggestedMoniker = "Test Moniker";
        //    var nickNamedEntityMock = new Mock<NicknamedEntity>();
        //    var dbContextMock = new Mock<TheFullStackTeamDbContext>();
        //    var dbSetMock = new Mock<DbSet<NicknamedEntity>>();
        //    dbSetMock.Setup(m => m.CountAsync(It.IsAny<Expression<Func<NicknamedEntity, bool>>>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(1);
        //    dbContextMock.Setup(m => m.Set<NicknamedEntity>()).Returns(dbSetMock.Object);

        //    var monikerService = new MonikerService(dbContextMock.Object);

        //    // Act
        //    var actualMoniker = await monikerService.FindValidMoniker<NicknamedEntity>(suggestedMoniker);

        //    // Assert
        //    Assert.Equal(expectedMoniker, actualMoniker);
        //}
    }
}