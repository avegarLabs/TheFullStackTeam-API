using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheFullStackTeam.Application.Institution.Command;
using TheFullStackTeam.Application.Institution.Results;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Institution.Handler
{
    public class CreateInstitutionCommandHandler : AppRequestHandler, IRequestHandler<CreateInstitutionCommand, InstitutionCommandResults>
    {
        private readonly IMonikerService _moniker;
        private readonly IConfiguration _configuration;
        public CreateInstitutionCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker, IConfiguration configuration) : base(context)
        {
            _moniker = moniker;
            _configuration = configuration;
        }

        public async Task<InstitutionCommandResults> Handle(CreateInstitutionCommand request, CancellationToken cancellationToken)
        {

            var country = _context.Countries.AsNoTracking().Where(c => c.Id.Equals(request.Model.Country.Id)).SingleOrDefault();
            if(country == null)
            {
                throw new Exception($"Country with id: {request.Model.Country.Id} not found");
            }

            var imageUrl = _configuration["OraganizationNoImage"];
            var institution = new Domain.Entities.Institution()
            {
                Name = request.Model.Name,
                Description = request.Model.Description,
                Moniker = await _moniker.FindValidMoniker<Domain.Entities.Institution>(request.Model.Name),
                Logo = new ImageUrl { ThumbUrl = imageUrl, DisplayUrl = imageUrl },
                City= request.Model.City,
                CountryId= country.Id,
             };

             _context.institutions.Add(institution);
            await _context.SaveChangesAsync(cancellationToken);
            return new InstitutionCommandResults(institution);
        }
    }
}
