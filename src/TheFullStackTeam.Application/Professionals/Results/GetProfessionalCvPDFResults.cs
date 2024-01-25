namespace TheFullStackTeam.Application.Professionals.Results
{
    public class GetProfessionalCvPDFResults : AppResult<string>
    {
        public GetProfessionalCvPDFResults(string url) : base(url)
        {
        }
    }
}
