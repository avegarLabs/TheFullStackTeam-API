using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType
{
    public class CreatedProfessionalJobTypeCommandResult : AppResult<ProfessionalJobTypeListItem>
    {
        public CreatedProfessionalJobTypeCommandResult(ProfessionalJobTypeListItem model) : base(model) { }
    }
}
