using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Organizations.Results;

namespace TheFullStackTeam.Application.Organizations.Queries
{
    public class ReadOrganizationDetailsQuery: IRequest<ReadOrganizationDetailsResults>
    {
        public string Moniker { get; set; }

        public ReadOrganizationDetailsQuery(string moniker)
        {
            Moniker = moniker;
        }
    }
}
