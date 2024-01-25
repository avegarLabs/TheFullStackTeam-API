using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;

using TheFullStackTeam.Application.Professionals.Commands.ProfesionalContractType;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalContractsType

{
    internal class CreateProfessionalContractTypeCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalContractTypeCommand, CreatedProfessionalContractTypeCommandResult>
    {
        public CreateProfessionalContractTypeCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {

        }

        public async Task<CreatedProfessionalContractTypeCommandResult> Handle(CreateProfessionalContractTypeCommand request, CancellationToken cancellationToken)
        {
            var professional = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
            if (professional == null)
            {
                throw new NotFoundException(nameof(Professional), request.ProfessionalId);
            }
            var professionalContract = new ProfessionalContractType()
            {
                Name = request.Model.Name,
                ProfessionalId = professional.Id

            };
            await _context.ProfessionalContractTypes.AddAsync(professionalContract);
            await _context.SaveChangesAsync(cancellationToken);
            return new CreatedProfessionalContractTypeCommandResult(professionalContract);
        }
    }

}
