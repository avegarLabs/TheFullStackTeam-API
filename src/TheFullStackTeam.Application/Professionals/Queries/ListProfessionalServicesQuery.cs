using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// A professional skills list query 
/// </summary>
public class ListProfessionalServicesQuery : IRequest<ListProfessionalServicesQueryResult>
{
    public Guid ProfessionalId { get; }

    public ListProfessionalServicesQuery(Guid id)
    {
        ProfessionalId = id;
    }
}

