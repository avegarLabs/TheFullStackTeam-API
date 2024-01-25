using MediatR;
using TheFullStackTeam.Application.Institution.Results;
using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.Application.Institution.Command
{
    public class CreateInstitutionCommand: IRequest<InstitutionCommandResults>
    {
        public InstitutionModel Model { get; set; }

        public CreateInstitutionCommand(InstitutionModel model)
        {
            Model = model;
        }
               
    }
}
