using MediatR;
using TheFullStackTeam.Application.Organizations.Results.OrganizationServicesResults;

namespace TheFullStackTeam.Application.Organizations.Queries.OrganizationServices;


public class ListOrganizationServicesQuery : IRequest<ListOrganizationServicesQueryResult>
{
    public Guid OrganizationId { get; }

    public ListOrganizationServicesQuery(Guid id)
    {
        OrganizationId = id;
    }
}

