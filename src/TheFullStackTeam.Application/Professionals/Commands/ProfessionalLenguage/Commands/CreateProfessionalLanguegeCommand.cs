using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Commands
{
    public class CreateProfessionalLanguegeCommand:IRequest<ProfessionalLanguageCommanResults>
    {
        public Guid ProfessionalId { get; set; }
        public ProfessionalLanguegeModel ProfessionalLanguegeModel { get; set; }
        
        public CreateProfessionalLanguegeCommand(Guid id, ProfessionalLanguegeModel model)
        {
            ProfessionalId = id;
            ProfessionalLanguegeModel= model;
        }
    }
}
