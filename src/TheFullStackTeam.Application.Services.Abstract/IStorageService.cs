namespace TheFullStackTeam.Application.Services.Abstract;

/// <summary>
/// Storage service
/// </summary>
public interface IStorageService : IService
{
    /// <summary>
    /// Store organization logo and return tuple with display image and thumb
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="moniker">Organization moniker</param>
    /// <param name="base64File"></param>
    /// <returns></returns>
    Task<(Uri displayImageUri, Uri thumbImageUri)> StoreOrganizationLogo(string base64File, string fileName, string moniker);

    /// <summary>
    /// Store a user profile image
    /// </summary>
    /// <param name="base64Image"></param>
    /// <param name="fileName"></param>
    /// <param name="moniker"></param>
    /// <returns></returns>
    Task<(Uri displayImageUri, Uri thumbImageUri)> StoreUserProfileImage(string base64Image, string fileName, string moniker);


    /// <summary>
    /// Created folder to save user profile image
    /// </summary>
    /// <param name="moniker"></param>
    /// <returns></returns>
    Task<bool> CreateDirectory(string moniker);


    Task<(Uri displayImageUri, Uri thumbImageUri)> StoreProfessionalVitaeFile(string vitaePdf, string fileName, string moniker);



}