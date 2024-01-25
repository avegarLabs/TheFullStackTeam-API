using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalContractType;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalContractsType
{
    public class DeleteProfesionalContractTypeCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalContractTypeCommand, DeleteProfessionalContractTypeCommandResult>
    {
        public DeleteProfesionalContractTypeCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<DeleteProfessionalContractTypeCommandResult> Handle(DeleteProfessionalContractTypeCommand request, CancellationToken cancellationToken)
        {
            var profesionalContracts = await _context.ProfessionalContractTypes.AsNoTracking()
              .Where(pc => pc.Id == request.ContratTypeId)
              .SingleOrDefaultAsync(cancellationToken);
            if (profesionalContracts == null)
            {
                throw new NotFoundException(nameof(ProfesionalContractsType), request.ContratTypeId);
            }
            _context.ProfessionalContractTypes.Remove(profesionalContracts);
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteProfessionalContractTypeCommandResult(true);
        }

    }
}

