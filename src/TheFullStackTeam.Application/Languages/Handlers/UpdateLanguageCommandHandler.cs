using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Languages.Commands;
using TheFullStackTeam.Application.Languages.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Languages.Handlers
{
    public class UpdateLanguageCommandHandler : AppRequestHandler, IRequestHandler<UpdateLanguageCommand, LanguageCommandResults>
    {
        public UpdateLanguageCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<LanguageCommandResults> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _context.Languages.Where(l => l.Id.Equals(request.LanguageId)).SingleOrDefaultAsync(cancellationToken);

            if (language == null)
            {
                throw new Exception($"Language id not found: {request.LanguageId}");
            }

            language.Name = request.Model.Name;
            language.LocalName = request.Model.LocalName;
            language.LCID = request.Model.LCID;
            language.IsoCode= request.Model.IsoCode;
            language.ThreeLetterIsoCode= request.Model.ThreeLetterIsoCode;

            _context.Languages.Update(language);
            await _context.SaveChangesAsync(cancellationToken);
            return new LanguageCommandResults(language);
        }
    }
}
