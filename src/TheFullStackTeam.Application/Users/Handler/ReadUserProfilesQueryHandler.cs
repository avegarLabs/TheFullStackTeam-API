using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Users.Queries;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;
using WindowsAzure.Table.Extensions;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class ReadUserProfilesQueryHandler : AppRequestHandler, IRequestHandler<ReadUserProfilesQuery, ReadUserProfilesQueryResults>
    {
        public ReadUserProfilesQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ReadUserProfilesQueryResults> Handle(ReadUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var user =  _context.Users.AsNoTracking().Where(u => u.AccountId.Equals(request.AccountId)).SingleOrDefault();
            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.User), request.AccountId);
            }

            var professional = await GetProfessional(user.Id);
            var organizations = await GetOrganizations(user.Id);

            var profiles = new UserProfilesListItem()
            {
                Professional = professional,
                OrganizationList = organizations
            };

            return new ReadUserProfilesQueryResults(profiles);
        }

        private async Task<ProfessionalListItem> GetProfessional(Guid userId)
        {
            return _context.Professionals
                .AsNoTracking()
                .Where(p => p.UserId.Equals(userId))
                .Select(ProfessionalListItem.Projection)
                .SingleOrDefault();
        }

        private async Task<List<OrganizationListItem>> GetOrganizations(Guid userId)
        {
            return _context.Organizations
                .AsNoTracking()
                .Where(o => o.UserId.Equals(userId))
                .Select(OrganizationListItem.Projection)
                .ToList();
        }
    }
}
