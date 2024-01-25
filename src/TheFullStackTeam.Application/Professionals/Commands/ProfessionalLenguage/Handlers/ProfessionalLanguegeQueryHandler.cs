using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Queries;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Handlers
{
    public class ProfessionalLanguegeQueryHandler : AppRequestHandler, IRequestHandler<ProfessionalLanguegeQuery, ProfessionalLanguegeQueryResults>
    {
        public ProfessionalLanguegeQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ProfessionalLanguegeQueryResults> Handle(ProfessionalLanguegeQuery request, CancellationToken cancellationToken)
        {
            var results = await _context.ProfessionalLanguages.Where(pl => pl.ProfessionalId.Equals(request.ProfessionalId)).AsNoTracking().Select(ProfessionalLanguegeListItem.Projection).ToListAsync(cancellationToken); 
            return new ProfessionalLanguegeQueryResults(results);
        }
    }
}
