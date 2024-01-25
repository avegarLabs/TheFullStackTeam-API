using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// A professional skills list query 
/// </summary>
public class ListProfessionalSalaryTypeQuery : IRequest<ListProfessionalSalaryQueryResult>
{
    public Guid ProfessionalId { get; }

    public ListProfessionalSalaryTypeQuery(Guid id)
    {
        ProfessionalId = id;
    }
}

