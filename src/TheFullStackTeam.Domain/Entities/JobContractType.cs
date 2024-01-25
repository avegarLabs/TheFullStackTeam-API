using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional Contract Type entity
/// </summary>
public class JobContractType : BaseEntity
{
    public const int ContractTypeNameMaxLenght = 150;

    public string ContractTypeName { get; set; } = null!;

    public Guid JobId { get; set; }
    public virtual Job Jobs { get; set; } = null!;
}