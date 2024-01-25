using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Commands;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Handlers
{
    public class DeleteProfessionalLanguegeCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalLanguegeCommand, ProfessionalLanguageDeleteResults>
    {
        public DeleteProfessionalLanguegeCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ProfessionalLanguageDeleteResults> Handle(DeleteProfessionalLanguegeCommand request, CancellationToken cancellationToken)
        {
            var profLanguage = await _context.ProfessionalLanguages.Where(pl => pl.Id.Equals(request.ProLanid) && pl.ProfessionalId.Equals(request.ProfessionalId)).SingleOrDefaultAsync(cancellationToken);
            if (profLanguage == null)
            {
                throw new NotFoundException(nameof(ProfessionalLanguage), request.ProLanid);
            }

            _context.ProfessionalLanguages.Remove(profLanguage);
            await _context.SaveChangesAsync(cancellationToken);
            return new ProfessionalLanguageDeleteResults(true);
        }
    }
}
