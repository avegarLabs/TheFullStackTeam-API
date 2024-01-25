using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType

{
    public class UpdateProfessionalSalaryTypeCommandResult : AppResult<ProfessionalSalaryTypeListItem>
    {
        public UpdateProfessionalSalaryTypeCommandResult(ProfessionalSalaryTypeListItem model) : base(model) { }
    }
}
