using TheFullStackTeam.Domain.ValueObjects;

namespace TheFullStackTeam.Application.Model.ValueObjects;

public class ImageUrlModel
{
    /// <summary>
    /// Thumbnail image url
    /// </summary>
    public string ThumbUrl { get; set; } = null!;

    /// <summary>
    /// Display image url
    /// </summary>
    public string DisplayUrl { get; set; } = null!;

    public static implicit operator ImageUrlModel?(ImageUrl? domainEntity)
    {
        return domainEntity != null
            ? new ImageUrlModel
            {
                ThumbUrl = domainEntity.ThumbUrl,
                DisplayUrl = domainEntity.DisplayUrl
            }
            : null;
    }
}