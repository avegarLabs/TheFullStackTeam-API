using System.Drawing;
using System.Drawing.Imaging;

namespace TheFullStackTeam.Extensions;

public static class ImageExtensions
{
    // extension method
    public static byte[] ToByteArray(this Image image)
    {
        using var ms = new MemoryStream();
        image.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }
}