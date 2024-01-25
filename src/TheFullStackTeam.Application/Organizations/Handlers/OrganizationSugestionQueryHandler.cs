using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Organizations.Queries;
using TheFullStackTeam.Application.Organizations.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Handlers
{
    public class OrganizationSugestionQueryHandler : AppRequestHandler, IRequestHandler<OrganizationSugestionQuery, OrganizationSugestionQueryResult>
    {
        public OrganizationSugestionQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<OrganizationSugestionQueryResult> Handle(OrganizationSugestionQuery request, CancellationToken cancellationToken)
        {
            var response = _context.Organizations
           .AsNoTracking()
           .Select(x => new SugestionListItem()
           {
               Name = x.Name,
               Moniker = x.Moniker,
               Type = "org"
           });

            var institutions = _context.institutions
                .AsNoTracking()
                .Select(x => new SugestionListItem()
                {
                    Name = x.Name,
                    Moniker = x.Moniker,
                    Type = "inst"
                });

            var results = response.Concat(institutions).ToList();

            return new OrganizationSugestionQueryResult(results);
        }
    }
}
