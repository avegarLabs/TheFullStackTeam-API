namespace TheFullStackTeam.Domain.Entities.Base;

/// <summary>
/// Nicknamed entity
/// </summary>
public class NicknamedEntity : BaseEntity
{
    public const int MonikerMaxLenght = 250;

    /// <summary>
    /// Identifier the entity
    /// </summary>
    public string Moniker { get; set; } = null!;

}