using MediatR;
using TheFullStackTeam.Application.Roles.Commands;
using TheFullStackTeam.Application.Roles.Results;
using TheFullStackTeam.Application.Services;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Roles.Handlers
{
    public class CreateRolesCommandHandler : AppRequestHandler, IRequestHandler<CreateRolesCommands, RolesCommandsResult>
    {
        private readonly IMonikerService _moniker;
        public CreateRolesCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _moniker = moniker;
        }

        public async Task<RolesCommandsResult> Handle(CreateRolesCommands request, CancellationToken cancellationToken)
        {
            Domain.Entities.Roles role = request.Model;
            role.Moniker = await _moniker.FindValidMoniker<Domain.Entities.Roles>(request.Model.Name);
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync(cancellationToken);
            return new RolesCommandsResult(role);
        }
    }
}
