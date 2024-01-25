using MediatR;
using TheFullStackTeam.Application.Jobs.Results;

namespace TheFullStackTeam.Application.Jobs.Queries
{
    public class ListJobsOrganizationQuery: IRequest<ListJobsQueryResults>
    {
        public Guid OrganizationId { get; set; }

       public ListJobsOrganizationQuery(Guid id)
        {
            OrganizationId = id;
        }
    }
}
