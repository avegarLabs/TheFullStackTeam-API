using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType
{
    public class ListProfessionalJobTypeCommandResult : AppResult<IEnumerable<ProfessionalJobTypeListItem>>
    {
        public ListProfessionalJobTypeCommandResult(IEnumerable<ProfessionalJobTypeListItem> result) : base(result) { }
    }
}
