using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.Base;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.GET;

public class ExperienceGet : NickNamedModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public DateTime? StartMonthYear { get; set; }
    public DateTime? EndMonthYear { get; set; }
    public Guid ProfessionalId { get; set; }

    public static implicit operator ExperienceGet(Position domainEntity) => new()
    {
        Name = domainEntity.Name,
        Description = domainEntity.Description,
        StartMonthYear = domainEntity.StartMonthYear,
        EndMonthYear = domainEntity.EndMonthYear,
        ProfessionalId = domainEntity.ProfessionalId,
    };

    public static Expression<Func<Position, ExperienceGet>> Projection =>
        x => new ExperienceGet
        {
            // Moniker = x.Moniker,
            Name = x.Name,
            Description = x.Description,
            StartMonthYear = x.StartMonthYear,
            EndMonthYear = x.EndMonthYear,
            ProfessionalId = x.ProfessionalId,
        };
}