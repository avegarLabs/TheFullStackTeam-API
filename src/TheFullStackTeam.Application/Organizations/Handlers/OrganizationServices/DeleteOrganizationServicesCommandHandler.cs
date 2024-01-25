using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Organizations.Commands.OrganizationsServices;
using TheFullStackTeam.Application.Organizations.Results.OrganizationServicesResults;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Handlers.OrganizationServices
{
    public class DeleteOrganizationServicesCommandHandler : AppRequestHandler, IRequestHandler<DeleteOrganizationServicesCommand, DeleteOrganizationServicesCommandResult>
    {
        public DeleteOrganizationServicesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<DeleteOrganizationServicesCommandResult> Handle(DeleteOrganizationServicesCommand request, CancellationToken cancellationToken)
        {
            var organizationService = await _context.OrganizationSevices
            .Where(ps => ps.Id == request.ServicesId && ps.OrganizationId == request.OrganizationId)
            .SingleOrDefaultAsync(cancellationToken);

            if (organizationService == null)
            {
                throw new NotFoundException(nameof(OrganizationSevices), request.ServicesId);
            }
            organizationService.ServiceSkills.Clear();
            _context.OrganizationSevices.Remove(organizationService);
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteOrganizationServicesCommandResult(true);
        }
    }
}
