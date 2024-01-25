namespace TheFullStackTeam.Application.Languages.Results
{
    public class LanguageDeleteCommandResults : AppResult<bool>
    {
        public LanguageDeleteCommandResults(bool model) : base(model)
        {
        }
    }
}
