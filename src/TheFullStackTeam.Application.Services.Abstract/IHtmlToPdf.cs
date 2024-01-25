namespace TheFullStackTeam.Application.Services.Abstract
{
    public interface IHtmlToPdf : IService
    {
       Task<string> WritePdf(string htmlTemplate, string moniker, string ident);
    }
}
