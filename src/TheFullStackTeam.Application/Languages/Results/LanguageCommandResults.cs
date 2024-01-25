using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Languages.Results
{
    public class LanguageCommandResults : AppResult<LanguageListItem>
    {
        public LanguageCommandResults(LanguageListItem model) : base(model)
        {
        }
    }
}
