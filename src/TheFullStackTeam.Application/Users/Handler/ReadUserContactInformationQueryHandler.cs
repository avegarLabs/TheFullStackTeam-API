using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Users.Queries;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class ReadUserContactInformationQueryHandler : AppRequestHandler, IRequestHandler<ReadUserContactInformationQuery, UserContactInformationResult>
    {
        public ReadUserContactInformationQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<UserContactInformationResult> Handle(ReadUserContactInformationQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(u => u.AccountId.Equals(request.AccountId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.AccountId);
            }
            return new UserContactInformationResult(user);
        }
    }

}
