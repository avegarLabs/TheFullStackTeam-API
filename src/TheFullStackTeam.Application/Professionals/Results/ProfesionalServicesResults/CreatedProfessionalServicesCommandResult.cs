using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults
{
    public class CreatedProfessionalServicesCommandResult : AppResult<ProfessionalServiceListItem>
    {
        public CreatedProfessionalServicesCommandResult(ProfessionalServiceListItem model) : base(model) { }

    }
}
