using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Organizations.Queries;
using TheFullStackTeam.Application.Organizations.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Handlers
{
    public class ReadOrganizationDetailsQueryHandler : AppRequestHandler, IRequestHandler<ReadOrganizationDetailsQuery, ReadOrganizationDetailsResults>
    {
        public ReadOrganizationDetailsQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ReadOrganizationDetailsResults> Handle(ReadOrganizationDetailsQuery request, CancellationToken cancellationToken)
        {
           var organization = await _context.Organizations.Where(o => o.Moniker.Equals(request.Moniker)).SingleOrDefaultAsync(cancellationToken);
            if(organization == null)
            {
                throw new NotFoundException(nameof(Organization), request.Moniker);
            }

            return new ReadOrganizationDetailsResults(organization);
        }
    }
}
