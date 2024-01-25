using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Extensions;

namespace TheFullStackTeam.Application.Services;

public class StorageService : IStorageService
{
    private const string ORGANIZATION_CONTAINER_REF = "organizations";

    private readonly ILogger<StorageService> _logger;
    private readonly IImageService _imageService;
    private readonly string _defaultConnection;

    public StorageService(ILogger<StorageService> logger, IConfiguration configuration, IImageService imageService)
    {
        _logger = logger;
        _imageService = imageService;
        _defaultConnection = configuration["BlobStorageDefaultConnection"] ??
                             throw new ArgumentException("[BlobStorageDefaultConnection] not found in configuration");
    }

    private async Task<Uri?> Store(Stream stream, string containerReference, string fileName)
    {
        if (!CloudStorageAccount.TryParse(_defaultConnection, out var storageAccount))
            return null;

        try
        {
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerReference.ToLower());
            await cloudBlobContainer.CreateIfNotExistsAsync();
            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
            { PublicAccess = BlobContainerPublicAccessType.Container });

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            await cloudBlockBlob.UploadFromStreamAsync(stream);

            if (cloudBlockBlob.Uri != null)
            {
                return cloudBlockBlob.Uri;
            }
        }
        catch (StorageException ex)
        {
            _logger.LogError(ex, $"Storage Exception while saving File (IFormFile) '{containerReference}'");
        }

        return null;
    }

    private async Task<Uri?> SaveAsync(IFormFile formFile, string containerReference, string fileName)
    {
        await using var stream = formFile.OpenReadStream();
        return await Store(stream, containerReference, fileName);
    }

    private async Task<Uri?> SaveAsync(byte[] image, string containerReference, string fileName)
    {
        await using var stream = new MemoryStream(image);
        return await Store(stream, containerReference, fileName);
    }

    private async Task<Uri?> SaveAsync(string base64Data, string containerReference, string fileName)
    {
        await using var stream = new MemoryStream(Convert.FromBase64String(base64Data));
        return await Store(stream, containerReference, fileName);
    }

    private static string BuildRandomFileName(string fileName, string moniker, bool isThumb)
    {
        var randomFileName = StringExtensions.RandomString(5);
        var suffix = isThumb ? $"-{randomFileName}.thumbnail" : $"-{randomFileName}";
        var storageFileName = $"{moniker}{suffix}{Path.GetExtension(fileName)}";
        return storageFileName;
    }

    public async Task<(Uri displayImageUri, Uri thumbImageUri)> StoreOrganizationLogo(string base64File, string fileName, string moniker)
    {
        var displayImageName = BuildRandomFileName(fileName, moniker, false);
        var displayImageUri = await SaveAsync(base64File, ORGANIZATION_CONTAINER_REF, displayImageName);

        if (displayImageUri == null)
        {
            return await Task.FromResult<(Uri displayImageUri, Uri thumbImageUri)>((null, null)!);
        }

        var thumbImage = await _imageService.CreateImageThumbnail(displayImageUri.ToString());
        var byteArrayThumb = thumbImage.ToByteArray();
        var thumbImageName = BuildRandomFileName(fileName, moniker, true);
        var thumbImageUri = await SaveAsync(byteArrayThumb, ORGANIZATION_CONTAINER_REF, thumbImageName);

        return (displayImageUri, thumbImageUri)!;
    }

    public async Task<(Uri displayImageUri, Uri thumbImageUri)> StoreUserProfileImage(string base64Image, string fileName, string moniker)
    {
        var displayImageName = BuildRandomFileName(fileName, moniker, false);
        var displayImageUri = await SaveAsync(base64Image, moniker, displayImageName);

        if (displayImageUri == null)
        {
            return await Task.FromResult<(Uri displayImageUri, Uri thumbImageUri)>((null, null)!);
        }

        var thumbImage = await _imageService.CreateImageThumbnail(displayImageUri.ToString());
        var byteArrayThumb = thumbImage.ToByteArray();
        var thumbImageName = BuildRandomFileName(fileName, moniker, true);
        var thumbImageUri = await SaveAsync(byteArrayThumb, moniker, thumbImageName);

        return (displayImageUri, thumbImageUri)!;
    }

    public async Task<bool> CreateDirectory(string folderName)
    {
        if (!CloudStorageAccount.TryParse(_defaultConnection, out var storageAccount))
            return false;

        try
        {
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(folderName);
            await cloudBlobContainer.CreateIfNotExistsAsync();
            var blockBlob = cloudBlobContainer.GetBlockBlobReference(folderName);
            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
            { PublicAccess = BlobContainerPublicAccessType.Container });

        }
        catch (StorageException ex)
        {
            _logger.LogError(ex, $"Storage Exception Creating Folder (IFolder) '{folderName}'");
        }

        return true;
    }

    public async Task<(Uri displayImageUri, Uri thumbImageUri)> StoreProfessionalVitaeFile(string vitaePdf, string fileName, string moniker)
    {
        var vitaeName = BuildRandomFileName(fileName, moniker, false);
        var displayVitaeUri = await SaveAsync(vitaePdf, moniker, fileName);

        if (displayVitaeUri == null)
        {
            return await Task.FromResult<(Uri displayVitaeUri, Uri thumbVitaeUri)>((null, null)!);
        }

        return (displayVitaeUri, null)!;
    }
}