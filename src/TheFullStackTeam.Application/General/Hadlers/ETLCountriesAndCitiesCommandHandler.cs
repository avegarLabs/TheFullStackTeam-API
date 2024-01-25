using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TheFullStackTeam.Application.General.Command;
using TheFullStackTeam.Application.General.Results;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.General.Hadlers
{
    public class ETLCountriesAndCitiesCommandHandler : AppRequestHandler, IRequestHandler<ETLCountriesAndCitiesCommand, ETLCommandResults>
    {
        public ETLCountriesAndCitiesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ETLCommandResults> Handle(ETLCountriesAndCitiesCommand request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Cities> cities = new List<Domain.Entities.Cities>();

            string jsonFilePath = @"C:\html\cities.json";
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);

            List<CityModel> cityModels = JsonConvert.DeserializeObject<List<CityModel>>(jsonContent);
            var countries = _context.Countries.ToList();

            foreach (var item in cityModels)
            {
               var country = GetCountry(countries, item);
                if(country != null)
                {
                    foreach(var c in item.cities)
                    {
                        if (c.Length > 100)
                        {
                            Console.WriteLine(c);
                        }
                        else { 
                            var city = new Domain.Entities.Cities()
                            {
                                Name = c,
                                CountryId = country.Id,
                            };
                            cities.Add(city);
                        }
                    }
                }
            }

            _context.Cities.AddRangeAsync(cities);
            await _context.SaveChangesAsync(cancellationToken);
            return new ETLCommandResults(true);


        }

        private Country GetCountry(List<Country> countries, CityModel item)
        {
            return countries.Find(c => c.CommonName.ToLower().Equals(item.country.ToLower()));
        }
    }
}

