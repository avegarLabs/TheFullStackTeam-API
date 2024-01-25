using MediatR;
using TheFullStackTeam.Application.Institution.Results;

namespace TheFullStackTeam.Application.Institution.Command
{
    public class DeleteInstitutionCommand: IRequest<InstitutionDeleteCommandResults>
    {
        public Guid InstitutionId { get; set; }

        public DeleteInstitutionCommand(Guid id)
        {
            InstitutionId = id;
        }
               
    }
}
