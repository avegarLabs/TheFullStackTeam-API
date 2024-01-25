using MediatR;
using TheFullStackTeam.Application.Jobs.Results;

namespace TheFullStackTeam.Application.Jobs.Queries
{
    public class ListJobsProfessionalQuery : IRequest<ListJobsQueryResults>
    {
        public Guid ProfessionalId { get; }

        public ListJobsProfessionalQuery(Guid id)
        {
            ProfessionalId = id;
        }
    }
}
