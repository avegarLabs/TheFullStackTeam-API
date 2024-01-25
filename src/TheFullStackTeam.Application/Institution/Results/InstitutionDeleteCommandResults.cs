namespace TheFullStackTeam.Application.Institution.Results
{
    public class InstitutionDeleteCommandResults : AppResult<bool>
    {
        public InstitutionDeleteCommandResults(bool model) : base(model)
        {
        }
    }
}
