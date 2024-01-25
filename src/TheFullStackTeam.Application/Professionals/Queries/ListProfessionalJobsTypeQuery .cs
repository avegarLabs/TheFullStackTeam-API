using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// A professional skills list query 
/// </summary>
public class ListProfessionalJobsTypeQuery : IRequest<ListProfessionalJobTypeCommandResult>
{
    public Guid ProfessionalId { get; }

    public ListProfessionalJobsTypeQuery(Guid id)
    {
        ProfessionalId = id;
    }
}

