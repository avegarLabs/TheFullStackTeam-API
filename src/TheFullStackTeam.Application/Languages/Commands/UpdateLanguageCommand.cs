using MediatR;
using TheFullStackTeam.Application.Languages.Results;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Languages.Commands
{
    public class UpdateLanguageCommand: IRequest<LanguageCommandResults>
    {
        public Guid LanguageId { get; set; }
        public LanguageListItem Model { get; set; }

        public UpdateLanguageCommand(Guid id, LanguageListItem model)
        {
            LanguageId = id;
            Model = model;
        }
    }
}
