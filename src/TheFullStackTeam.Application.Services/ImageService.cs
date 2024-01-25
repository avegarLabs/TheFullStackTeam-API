using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime;
using TheFullStackTeam.Application.Services.Abstract;

namespace TheFullStackTeam.Application.Services;

public class ImageService : IImageService
{
    private const int MaxRecommendedWidth = 1280;
    private const int MaxRecommendedHeight = 720;
    private readonly ILogger<ImageService> _logger;
    private readonly int _thumbnailHeight;
    private readonly int _thumbnailWidth;

    public ImageService(ILogger<ImageService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _thumbnailHeight = int.Parse(configuration["ThumbnailHeight"] ?? throw new ArgumentException("[ThumbnailHeight] not found in configuration"));
        _thumbnailWidth = int.Parse(configuration["ThumbnailWidth"] ?? throw new ArgumentException("[ThumbnailWidth] not found in configuration"));
    }

    private async Task<Image> LoadImageFromUrl(string url)
    {
        try
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(url);
            await using var inputStream = await response.Content.ReadAsStreamAsync();
            using var temp = new Bitmap(inputStream);
            return new Bitmap(temp);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Loading image from: {url}", ex);
            throw;
        }
    }

        private Image ResizeImage(Image sourceImage, int maxWidth, int maxHeight)
        {
            try
            {
                var originWith = sourceImage.Width;
                var originHeight = sourceImage.Height;

                var xRatio = (float) originWith / maxWidth;
                var yRatio = (float) originHeight / maxHeight;
                var finalRatio = xRatio >= yRatio ? xRatio : yRatio;

                var finalWidth = (int) Math.Round(originWith / finalRatio);
                var finalHeight = (int) Math.Round(originHeight / finalRatio);

                var b = new Bitmap(finalWidth, finalHeight);
                var g = Graphics.FromImage(b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourceImage, 0, 0, finalWidth, finalHeight);
                g.Dispose();

                return b;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    public async Task<Image> CreateImageThumbnail(string url)
    {
        using var sourceImage = await LoadImageFromUrl(url);
        try
        {
            return ResizeImage(sourceImage, _thumbnailWidth, _thumbnailHeight);
        }

        catch (Exception ex)
        {
            _logger.LogError($"Error Generating thumbnail: {url}", ex);
            throw;
        }
    }



    public async Task<byte[]> OptimizeImageForWeb(IFormFile file)
    {
        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        using var img = Image.FromStream(memoryStream);
        var resizedImage = ResizeImage(img, MaxRecommendedWidth, MaxRecommendedHeight);
        var converter = new ImageConverter();
        return (byte[])converter.ConvertTo(resizedImage, typeof(byte[]));
    }

    public bool FileIsAnImage(string extension)
    {
        var imageExtensions = new List<string> { ".jpg", ".bmp", ".gif", ".png", ".jpeg" };
        return imageExtensions.Contains(extension.ToLowerInvariant());
    }
}