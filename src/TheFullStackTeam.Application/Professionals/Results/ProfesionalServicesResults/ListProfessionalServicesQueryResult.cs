using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults
{
    public class ListProfessionalServicesQueryResult : AppResult<IEnumerable<ProfessionalServiceListItem>>
    {
        public ListProfessionalServicesQueryResult(IEnumerable<ProfessionalServiceListItem> model) : base(model) { }
    }
}
