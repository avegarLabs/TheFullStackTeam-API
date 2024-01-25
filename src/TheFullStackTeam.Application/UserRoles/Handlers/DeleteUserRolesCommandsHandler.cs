using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.UserRoles.Commands;
using TheFullStackTeam.Application.UserRoles.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.UserRoles.Handlers
{
    public class DeleteUserRolesCommandsHandler : AppRequestHandler, IRequestHandler<DeleteUserRolesCommands, DeleteUserRolesCommandResults>
    {
        public DeleteUserRolesCommandsHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<DeleteUserRolesCommandResults> Handle(DeleteUserRolesCommands request, CancellationToken cancellationToken)
        {
            var userRole = await _context.UserRole.Where(ur => ur.UserId.Equals(request.UserId) && ur.Id.Equals(request.RoleId)).SingleOrDefaultAsync(cancellationToken);
            if (userRole == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.UserRoles), request.RoleId);
            }

            _context.UserRole.Remove(userRole);
            return new DeleteUserRolesCommandResults(true);

        }
    }
}
