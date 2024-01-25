using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.POST;

public class TitlePost
{
    public string Name { get; set; } = null!;
    public string OrganizationName { get; set; } = null!;
    public DateTime StartMonthYear { get; set; }
    public DateTime? EndMonthYear { get; set; }
    public Guid? OrganizationId { get; set; }

    public static implicit operator Title(TitlePost model) => new()
    {
        Name = model.Name,
        OrganizationName = model.OrganizationName,
        StartMonthYear = model.StartMonthYear,
        EndMonthYear = model.EndMonthYear,
        OrganizationId = model.OrganizationId,
    };
}