using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType
{
    public class ListProfessionalSalaryQueryResult : AppResult<IEnumerable<ProfessionalSalaryTypeListItem>>
    {
        public ListProfessionalSalaryQueryResult(IEnumerable<ProfessionalSalaryTypeListItem> result) : base(result) { }
    }
}
