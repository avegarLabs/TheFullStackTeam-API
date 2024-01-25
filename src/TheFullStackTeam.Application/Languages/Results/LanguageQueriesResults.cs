using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Languages.Results
{
    public class LanguageQueriesResults : AppResult<IEnumerable<LanguageListItem>>
    {
        public LanguageQueriesResults(IEnumerable<LanguageListItem> model) : base(model)
        {
        }
    }
}
