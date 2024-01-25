using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheFullStackTeam.Domain.Entities.Base;

/// <summary>
/// Entity base
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// The entity identifier
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    /// <value>
    /// The created.
    /// </value>
    [Required]
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the last modification date.
    /// </summary>
    /// <value>
    /// The modified.
    /// </value>
    public DateTime? Modified { get; set; }

    /// <summary>
    /// Gets or sets the last modification date.
    /// </summary>
    /// <value>
    /// The modified.
    /// </value>
    public DateTime? DeletionDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is deleted.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
    /// </value>
    public bool IsDeleted { get; set; }

    public void Reset()
    {
        Created = DateTime.UtcNow;
        Id = Guid.Empty;
        DeletionDate = null;
        IsDeleted = false;
    }
}