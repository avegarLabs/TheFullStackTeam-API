using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType

{
    public class UpdateProfessionalJobTypeCommandResult : AppResult<ProfessionalJobTypeListItem>
    {
        public UpdateProfessionalJobTypeCommandResult(ProfessionalJobTypeListItem model) : base(model) { }
    }
}
