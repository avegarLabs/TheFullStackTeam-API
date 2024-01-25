using MediatR;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Queries
{
    public class ProfessionalLanguegeQuery:IRequest<ProfessionalLanguegeQueryResults>
    {
        public Guid ProfessionalId { get; set; }

        public ProfessionalLanguegeQuery(Guid id)
        {
            ProfessionalId = id;
        }
    }
}
