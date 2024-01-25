using MediatR;
using TheFullStackTeam.Application.UserRoles.Commands;
using TheFullStackTeam.Application.UserRoles.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.UserRoles.Handlers
{
    public class CreateUserRolesCommandHandler : AppRequestHandler, IRequestHandler<CreateUserRolesCommands, CreateUserRolesCommandsResults>
    {
        public CreateUserRolesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<CreateUserRolesCommandsResults> Handle(CreateUserRolesCommands request, CancellationToken cancellationToken)
        {
            var userRole = new Domain.Entities.UserRoles()
            {
                RoleName = request.Model.Name,
                UserId = request.UserId,
                RoleId = request.Model.Id
            };
            await _context.UserRole.AddAsync(userRole);
            await _context.SaveChangesAsync(cancellationToken);
            return new CreateUserRolesCommandsResults(userRole);
        }
    }
}
