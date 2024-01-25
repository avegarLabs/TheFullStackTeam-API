using MediatR;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Commands
{
    public class UpdateProfessionalLanguageCommand:IRequest<ProfessionalLanguageCommanResults>
    {
        public Guid ProfessionalId { get; set; }
        public Guid ProLangId { get; set; }
        public ProfessionalLanguegeListItem Model { get; set; }

        public UpdateProfessionalLanguageCommand(Guid id, Guid plId, ProfessionalLanguegeListItem model)
        {
            ProfessionalId = id;
            ProLangId= plId;
            Model = model;
        }

    }
}
