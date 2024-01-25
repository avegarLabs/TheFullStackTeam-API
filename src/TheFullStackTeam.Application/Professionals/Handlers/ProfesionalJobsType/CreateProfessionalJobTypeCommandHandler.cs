using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalJobType;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalJobsType

{
    public class CreateProfessionalJobTypeCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalJobTypeCommand, CreatedProfessionalJobTypeCommandResult>
    {
        private readonly IMonikerService _monikerService;

        public CreateProfessionalJobTypeCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService) : base(context)
        {
            _monikerService = monikerService;
        }

        public async Task<CreatedProfessionalJobTypeCommandResult> Handle(CreateProfessionalJobTypeCommand request, CancellationToken cancellationToken)
        {
            var professional = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
            if (professional == null)
            {
                throw new NotFoundException(nameof(Professional), request.ProfessionalId);
            }
            var professionalJobType = new Domain.Entities.ProfessionalJobType()
            {
                Name = request.Model.Name,
                ProfessionalId = professional.Id
            };

            await _context.ProfessionalJobTypes.AddAsync(professionalJobType);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreatedProfessionalJobTypeCommandResult(professionalJobType);
        }
    }

}
