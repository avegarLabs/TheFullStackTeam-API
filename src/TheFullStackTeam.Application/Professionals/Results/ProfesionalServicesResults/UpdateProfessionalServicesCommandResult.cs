using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults
{
    public class UpdateProfessionalServicesCommandResult : AppResult<ProfessionalServiceListItem>
    {
        public UpdateProfessionalServicesCommandResult(ProfessionalServiceListItem model) : base(model) { }
    }
}
