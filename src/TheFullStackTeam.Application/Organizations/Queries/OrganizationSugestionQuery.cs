using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Organizations.Results;

namespace TheFullStackTeam.Application.Organizations.Queries
{
    public class OrganizationSugestionQuery: IRequest<OrganizationSugestionQueryResult>
    {
    }
}
