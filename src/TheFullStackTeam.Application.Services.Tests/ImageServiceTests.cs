using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Services;

namespace TheFullStackTeam.Apllication.Services.Tests
{
    public class ImageServiceTests
    {
        private readonly Mock<ILogger<ImageService>> _loggerMock = new();


        [Fact]
        public async Task CreateImageThumbnail_ValidUrl_ReturnsResizedImage()
        {
            // Arrange
            var url = "https://thefullstackteam.com/wp-content/uploads/2022/08/Full-Stack-Team.jpg";
            var expectedMaxWidth = 128;
            var expectedMaxHeight = 72;
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"ThumbnailHeight", expectedMaxHeight.ToString()},
                    {"ThumbnailWidth", expectedMaxWidth.ToString()}
                })
                .Build();
            var imageService = new ImageService(_loggerMock.Object, configuration);

            // Act
            var thumbnail = await imageService.CreateImageThumbnail(url);

            // Assert
            Assert.NotNull(thumbnail);
            Assert.True(thumbnail.Width <= expectedMaxWidth);
            Assert.True(thumbnail.Height <= expectedMaxHeight);
        }
    }
}
