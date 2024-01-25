using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalPositions
{
    public class UpdateProfessionalPositionsCommandResult : AppResult<PositionListItem>
    {
        public UpdateProfessionalPositionsCommandResult(PositionListItem model) : base(model)
        {
        }
    }
}
