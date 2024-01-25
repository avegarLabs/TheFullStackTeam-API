using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Languages.Commands;
using TheFullStackTeam.Application.Languages.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Languages.Handlers
{
    public class DeleteLanguageCommandHandler : AppRequestHandler, IRequestHandler<DeleteLanguageCommand, LanguageDeleteCommandResults>
    {
        public DeleteLanguageCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<LanguageDeleteCommandResults> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _context.Languages.Where(l => l.Id.Equals(request.LanguageId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);

            if(language == null)
            {
             throw new Exception($"Language id not found: {request.LanguageId}");
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync(cancellationToken);
            return new LanguageDeleteCommandResults(true);
        }
    }
}
