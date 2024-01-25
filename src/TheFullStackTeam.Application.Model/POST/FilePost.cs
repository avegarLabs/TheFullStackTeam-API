namespace TheFullStackTeam.Application.Model.POST;

/// <summary>
/// Api model for POST request
/// </summary>
public class FilePost
{
    /// <summary>
    /// File name. Example: "file.txt"
    /// </summary>
    public string FileName { get; set; } = null!;

    /// <summary>
    /// Base 64 encoded file content. Example: "SGVsbG8gV29ybGQh"
    /// </summary>
    public string Base64File { get; set; } = null!;
}