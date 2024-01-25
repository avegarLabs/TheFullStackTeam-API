using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType
{
    public class CreatedProfessionalSalaryTypeCommandResult : AppResult<ProfessionalSalaryTypeListItem>
    {
        public CreatedProfessionalSalaryTypeCommandResult(ProfessionalSalaryTypeListItem model) : base(model) { }
    }
}
