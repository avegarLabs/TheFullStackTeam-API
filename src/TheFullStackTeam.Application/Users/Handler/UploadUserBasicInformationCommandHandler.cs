using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class UploadUserBasicInformationCommandHandler : AppRequestHandler, IRequestHandler<UpdateUserBasicInformationCommand, ReadUserProfileInformationResult>
    {
        public UploadUserBasicInformationCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ReadUserProfileInformationResult> Handle(UpdateUserBasicInformationCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(u => u.AccountId.Equals(request.AccountId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(Users), request.AccountId);
            }

            user.Name = request.Model.Name;
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return new ReadUserProfileInformationResult(user);
        }
    }
}
