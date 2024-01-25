using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Commands;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Handlers
{
    public class UpdateProfessionalLanguageCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalLanguageCommand, ProfessionalLanguageCommanResults>
    {
        public UpdateProfessionalLanguageCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ProfessionalLanguageCommanResults> Handle(UpdateProfessionalLanguageCommand request, CancellationToken cancellationToken)
        {
            var profLanguege = await _context.ProfessionalLanguages.Where(pl => pl.ProfessionalId.Equals(request.ProfessionalId) && pl.Id.Equals(request.ProLangId)).SingleOrDefaultAsync(cancellationToken);
            if (profLanguege == null)
            {
                throw new NotFoundException(nameof(ProfessionalLanguage), request.ProLangId);
            }
            profLanguege.Level = request.Model.Level;
            profLanguege.LanguegeId= request.Model.LanguageId;
            profLanguege.LanguegeName = request.Model.Name;
            _context.ProfessionalLanguages.Update(profLanguege);
            await _context.SaveChangesAsync(cancellationToken);
            return new ProfessionalLanguageCommanResults(profLanguege);
        }
    }
}
