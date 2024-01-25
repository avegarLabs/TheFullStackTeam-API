using MediatR;
using TheFullStackTeam.Application.Institution.Results;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Institution.Command
{
    public class UpdateInstitutionCommand: IRequest<InstitutionCommandResults>
    {
        public Guid InstitutionId { get; set; }
        public InstitutionListItem Model { get; set; }

        public UpdateInstitutionCommand(Guid  id, InstitutionListItem model)
        {
            InstitutionId = id;
            Model = model;
        }
    }
}
