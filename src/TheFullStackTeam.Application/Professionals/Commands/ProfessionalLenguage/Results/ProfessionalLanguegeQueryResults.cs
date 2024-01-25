using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results
{
    public class ProfessionalLanguegeQueryResults : AppResult<IEnumerable<ProfessionalLanguegeListItem>>
    {
        public ProfessionalLanguegeQueryResults(IEnumerable<ProfessionalLanguegeListItem> model) : base(model)
        {
        }
    }
}
