using MediatR;
using Newtonsoft.Json;
using TheFullStackTeam.Application.General.Command;
using TheFullStackTeam.Application.General.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.General.Hadlers
{
    public class ETLCountriesCommandHandler : AppRequestHandler, IRequestHandler<ETLCountriesCommand, ETLCommandResults>
    {
        public ETLCountriesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ETLCommandResults> Handle(ETLCountriesCommand request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Country> countries = new List<Domain.Entities.Country>();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://restcountries.com/v3.1/all");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(content);

                foreach (var item in json)
                {
                    if (item["name"]["official"] != null && item["name"]["common"] != null && item["cca2"] != null && item["cca3"] != null && item["ccn3"] != null && item["tld"].ToString() != null)
                    {
                        var country = new Domain.Entities.Country()
                        {

                            OfficialName = item["name"]["official"],
                            CommonName = item["name"]["common"],
                            Cca2 = item["cca2"],
                            Cca3 = item["cca3"],
                            Ccn3 = item["ccn3"],
                            NativeName = item["name"]["official"],
                            Tld = "." + item["cca2"].ToString().ToLower(),
                        };
                        countries.Add(country);
                    }
                }
            }

            await _context.Countries.AddRangeAsync(countries);
            await _context.SaveChangesAsync(cancellationToken);
            return new ETLCommandResults(true);


        }
    }
}

