using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Organizations.Results
{
    public class OrganizationSugestionQueryResult: AppResult<IEnumerable<SugestionListItem>>
    {

        public OrganizationSugestionQueryResult(IEnumerable<SugestionListItem> model): base(model) { }
    }
}
