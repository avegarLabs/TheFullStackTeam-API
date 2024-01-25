using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Organizations.Queries.OrganizationServices;
using TheFullStackTeam.Application.Organizations.Results.OrganizationServicesResults;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Handlers.organizationServices
{
    public class ListOrganizationServicesQueryHandler : IRequestHandler<ListOrganizationServicesQuery, ListOrganizationServicesQueryResult>
    {
        private readonly TheFullStackTeamDbContext _contex;


        public ListOrganizationServicesQueryHandler(TheFullStackTeamDbContext contex)
        {
            _contex = contex;
        }

        public async Task<ListOrganizationServicesQueryResult> Handle(ListOrganizationServicesQuery request, CancellationToken cancellationToken)
        {
            var servcesList = await _contex.OrganizationSevices.Where(sv => sv.OrganizationId == request.OrganizationId).Select(OrganizationServiceListItem.Projection).ToListAsync(cancellationToken);
            return new ListOrganizationServicesQueryResult(servcesList);
        }
    }
}
