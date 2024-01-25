using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.General.Results;

namespace TheFullStackTeam.Application.General.Command
{
    public class ETLCountriesCommand:IRequest<ETLCommandResults>
    {
    }
}
