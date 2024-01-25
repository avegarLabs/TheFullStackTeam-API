using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFullStackTeam.Application.General.Results
{
    public class ETLCommandResults:AppResult<bool>
    {
        public ETLCommandResults(bool success): base(success) { }
    }
}
