using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Queries;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalServices
{
    public class ListProfessionalServicesQueryHandler : IRequestHandler<ListProfessionalServicesQuery, ListProfessionalServicesQueryResult>
    {
        private readonly TheFullStackTeamDbContext _contex;


        public ListProfessionalServicesQueryHandler(TheFullStackTeamDbContext contex)
        {
            _contex = contex;
        }

        public async Task<ListProfessionalServicesQueryResult> Handle(ListProfessionalServicesQuery request, CancellationToken cancellationToken)
        {
            var servcesList = await _contex.ProfessionalSevices.Where(sv => sv.ProfessionalId == request.ProfessionalId).Select(ProfessionalServiceListItem.Projection).ToListAsync(cancellationToken);
            return new ListProfessionalServicesQueryResult(servcesList);
        }
    }
}
