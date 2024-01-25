using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Jobs.Results;

namespace TheFullStackTeam.Application.Jobs.Queries
{
    public class ListJobsQuery:IRequest<ListJobsQueryResults>
    {
    }
}
