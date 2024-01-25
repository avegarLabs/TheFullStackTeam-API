using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace TheFullStackTeam.Application.Services.Abstract;

public interface IImageService : IService
{
    Task<Image> CreateImageThumbnail(string url);
    Task<byte[]> OptimizeImageForWeb(IFormFile file);
    bool FileIsAnImage(string extension);
}

