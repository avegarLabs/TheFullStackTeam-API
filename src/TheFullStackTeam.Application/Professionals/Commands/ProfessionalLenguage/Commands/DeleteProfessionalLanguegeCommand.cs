using MediatR;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Commands
{
    public class DeleteProfessionalLanguegeCommand:IRequest<ProfessionalLanguageDeleteResults>
    {
        public Guid ProfessionalId { get; set; }
        public Guid ProLanid { get; set; }
        public DeleteProfessionalLanguegeCommand(Guid id, Guid idPL)
        {
            ProfessionalId = id;
            ProLanid = idPL;
        }
     }
}
