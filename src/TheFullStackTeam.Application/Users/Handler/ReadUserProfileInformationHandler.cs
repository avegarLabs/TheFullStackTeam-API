using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Services;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Application.Users.Queries;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;



namespace TheFullStackTeam.Application.Users.Handler
{
    public class ReadUserProfileInformationHandler : AppRequestHandler, IRequestHandler<ReadUserProfileInformationQuery, ReadUserProfileInformationResult>
    {

        public ReadUserProfileInformationHandler(TheFullStackTeamDbContext context) : base(context)
        {}

        public async Task<ReadUserProfileInformationResult> Handle(ReadUserProfileInformationQuery request, CancellationToken cancellationToken)
        {
           
            var user = _context.Users.Where(up => up.AccountId.Equals(request.AccountId)).SingleOrDefault();

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.AccountId);
            }
            return new ReadUserProfileInformationResult(user);
        }
    }
}
