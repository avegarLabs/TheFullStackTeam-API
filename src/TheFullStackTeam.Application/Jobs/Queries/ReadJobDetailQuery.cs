using MediatR;
using TheFullStackTeam.Application.Jobs.Results;

namespace TheFullStackTeam.Application.Jobs.Queries
{
    public class ReadJobDetailQuery: IRequest<ReadJobDetailQueryResults>
    {
        public string JobMoniker { get; set; }

        public ReadJobDetailQuery(string moniker)
        {
            JobMoniker = moniker;
        }
    }
}
