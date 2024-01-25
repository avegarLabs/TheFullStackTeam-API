using MediatR;
using TheFullStackTeam.Application.Professionals.Results;

namespace TheFullStackTeam.Application.Professionals.Queries
{
    public class ListJobsSugestionsQuery: IRequest<ListJobSugestionQueryResults>
    {
        public Guid professionalId { get; set; }

        public ListJobsSugestionsQuery(Guid id)
        {
            this.professionalId = id;
        }
    }
}
