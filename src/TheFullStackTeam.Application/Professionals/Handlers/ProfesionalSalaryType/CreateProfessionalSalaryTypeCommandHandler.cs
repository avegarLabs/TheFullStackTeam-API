using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalSalaryType;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalSalaryType

{
    public class CreateProfessionalSalaryTypeCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalSalaryTypeCommand, CreatedProfessionalSalaryTypeCommandResult>
    {

        private readonly IMonikerService _monikerService;
        public CreateProfessionalSalaryTypeCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService) : base(context)
        {
            _monikerService = monikerService;
        }

        public async Task<CreatedProfessionalSalaryTypeCommandResult> Handle(CreateProfessionalSalaryTypeCommand request, CancellationToken cancellationToken)
        {
            var professional = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
            if (professional == null)
            {
                throw new NotFoundException(nameof(Professional), professional.Id);
            }

            var professionalSalaryType = new Domain.Entities.ProfessionalSalaryType()
            {
                PaymentPeriod = request.Model.PaymentPeriod,
                Amount = request.Model.Amount,
                Currency = request.Model.Currency,
                ProfessionalId = professional.Id,

            };

            await _context.ProfessionalSalaryTypes.AddAsync(professionalSalaryType);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreatedProfessionalSalaryTypeCommandResult(professionalSalaryType);
        }
    }

}
