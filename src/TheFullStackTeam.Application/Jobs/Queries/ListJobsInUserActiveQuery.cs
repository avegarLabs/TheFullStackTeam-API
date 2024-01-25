using MediatR;
using TheFullStackTeam.Application.Jobs.Results;

namespace TheFullStackTeam.Application.Jobs.Queries
{
    public class ListJobsInUserActiveQuery: IRequest<ListJobsQueryResults>
    {
        public string AccountId { get; set; }

        public ListJobsInUserActiveQuery(string account) {
            AccountId= account;
        }
    }
}
