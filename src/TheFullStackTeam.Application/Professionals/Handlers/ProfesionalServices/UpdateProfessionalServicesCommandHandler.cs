using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalServices
{
    public class UpdateProfessionalServicesTypeCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalServicesCommand, UpdateProfessionalServicesCommandResult>
    {
        public UpdateProfessionalServicesTypeCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<UpdateProfessionalServicesCommandResult> Handle(UpdateProfessionalServicesCommand request, CancellationToken cancellationToken)
        {
            var professionalServices = await _context.ProfessionalSevices.AsNoTracking().Where(ps => ps.Id == request.ServiceId && ps.ProfessionalId == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
            if (professionalServices == null)
            {
                throw new NotFoundException(nameof(ProfessionalSevices), professionalServices.ServiceName);
            }

            professionalServices.ServiceName = request.Model.ServicesName;
            professionalServices.SevicePrice = request.Model.Price;
            professionalServices.Currency = request.Model.Currency;
            professionalServices.ServiceDescription = request.Model.ServiceDescription;
            _context.ProfessionalSevices.Update(professionalServices);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateProfessionalServicesCommandResult(professionalServices);

        }
    }
}
