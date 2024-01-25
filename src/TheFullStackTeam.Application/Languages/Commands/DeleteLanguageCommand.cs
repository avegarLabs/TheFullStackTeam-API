using MediatR;
using TheFullStackTeam.Application.Languages.Results;

namespace TheFullStackTeam.Application.Languages.Commands
{
    public class DeleteLanguageCommand: IRequest<LanguageDeleteCommandResults>
    {

        public Guid LanguageId { get; set; }

        public DeleteLanguageCommand(Guid id)
        {
            LanguageId = id;
        }
    }
}
