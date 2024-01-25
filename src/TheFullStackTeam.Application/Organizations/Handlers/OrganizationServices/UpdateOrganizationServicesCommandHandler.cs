using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Organizations.Commands.OrganizationServices;
using TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Handlers.OrganizationServices
{
    public class UpdateOrganizationServicesCommandHandler : AppRequestHandler, IRequestHandler<UpdateOrganizationServicesCommand, UpdateOrganizationServicesCommandResult>
    {
        public UpdateOrganizationServicesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<UpdateOrganizationServicesCommandResult> Handle(UpdateOrganizationServicesCommand request, CancellationToken cancellationToken)
        {
            var organizationServices = await _context.OrganizationSevices.AsNoTracking().Where(ps => ps.Id == request.ServiceId && ps.OrganizationId == request.OrganizationId).SingleOrDefaultAsync(cancellationToken);
            if (organizationServices == null)
            {
                throw new NotFoundException(nameof(OrganizationSevices), request.ServiceId);
            }

            organizationServices.ServiceName = request.Model.ServicesName;
            organizationServices.SevicePrice = request.Model.Price;
            organizationServices.Currency = request.Model.Currency;
            organizationServices.ServiceDescription = request.Model.ServiceDescription;
            _context.OrganizationSevices.Update(organizationServices);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateOrganizationServicesCommandResult(organizationServices);

        }
    }
}
