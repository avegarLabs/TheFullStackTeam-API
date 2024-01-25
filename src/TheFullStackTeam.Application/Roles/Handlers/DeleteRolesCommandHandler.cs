using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Roles.Commands;
using TheFullStackTeam.Application.Roles.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Roles.Handlers
{
    public class DeleteRolesCommandHandler : AppRequestHandler, IRequestHandler<DeleteRolesCommands, RolesDeleteResults>
    {
        public DeleteRolesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<RolesDeleteResults> Handle(DeleteRolesCommands request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.Where(r => r.Id.Equals(request.RoleId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if(role == null)
            {
                throw new NotFoundException(nameof(Roles), request.RoleId);
            }

           _context.Remove(role);
            await _context.SaveChangesAsync(cancellationToken);
            return new RolesDeleteResults(true);


        }
    }
}
