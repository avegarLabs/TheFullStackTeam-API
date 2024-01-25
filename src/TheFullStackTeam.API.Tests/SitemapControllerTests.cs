using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TheFullStackTeam.API.Controllers;
using TheFullStackTeam.Application.Sitemap;

namespace TheFullStackTeam.API.Tests
{
    public class SitemapControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithExpectedValue()
        {
            // ARRANGE
            var expected = new OkObjectResult("test");

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetSitemap>(), CancellationToken.None))
                         .ReturnsAsync(expected);

            var services = new ServiceCollection();
            services.AddSingleton(_mediatorMock.Object);
            var serviceProvider = services.BuildServiceProvider();

            var controller = new SitemapController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { RequestServices = serviceProvider }
                }
            };

            // ACT
            var result = await controller.Get();

            // ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("test", okResult.Value);
        }
    }

}