using MediatR;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Commands;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Handlers
{
    public class CreateProfessionalLanguageCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalLanguegeCommand, ProfessionalLanguageCommanResults>
    {
        public CreateProfessionalLanguageCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ProfessionalLanguageCommanResults> Handle(CreateProfessionalLanguegeCommand request, CancellationToken cancellationToken)
        {
            var professionalLanguage = new Domain.Entities.ProfessionalLanguage()
            {
                LanguegeName = request.ProfessionalLanguegeModel.Language.Name,
                Level = request.ProfessionalLanguegeModel.Level,
                ProfessionalId = request.ProfessionalId,
                LanguegeId = request.ProfessionalLanguegeModel.Language.Id
            };
            await _context.ProfessionalLanguages.AddAsync(professionalLanguage);
            await _context.SaveChangesAsync(cancellationToken);
            return new ProfessionalLanguageCommanResults(professionalLanguage);
        }
    }
}
