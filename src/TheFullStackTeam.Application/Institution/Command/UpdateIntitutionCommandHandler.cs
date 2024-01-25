using MediatR;
using TheFullStackTeam.Application.Institution.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Institution.Command
{
    public class UpdateIntitutionCommandHandler : AppRequestHandler, IRequestHandler<UpdateInstitutionCommand, InstitutionCommandResults>
    {
        public UpdateIntitutionCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<InstitutionCommandResults> Handle(UpdateInstitutionCommand request, CancellationToken cancellationToken)
        {
            var institution = _context.institutions.Where(i => i.Id.Equals(request.InstitutionId)).SingleOrDefault();

            if (institution == null)
            {
                throw new Exception($"Institution Id: {request.InstitutionId} not fount");
            }

            institution.Name = request.Model.Name;
            institution.Description = request.Model.Description;
            institution.City = request.Model.City;
            institution.CountryId = request.Model.Country.Id;

            _context.institutions.Update(institution);
            await _context.SaveChangesAsync(cancellationToken);
            return new InstitutionCommandResults(institution);
        }
    }
}
