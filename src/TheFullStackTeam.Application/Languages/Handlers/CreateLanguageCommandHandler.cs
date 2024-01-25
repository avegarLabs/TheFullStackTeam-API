using MediatR;
using TheFullStackTeam.Application.Languages.Commands;
using TheFullStackTeam.Application.Languages.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Languages.Handlers
{
    public class CreateLanguageCommandHandler : AppRequestHandler, IRequestHandler<CreateLanguageCommand, LanguageCommandResults>
    {
        public CreateLanguageCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<LanguageCommandResults> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Language languages = request.Model;
            await _context.Languages.AddAsync(languages);
            await _context.SaveChangesAsync(cancellationToken);
            return new LanguageCommandResults(languages);
        }
    }
}
