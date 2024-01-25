using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Queries;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalSalaryType
{
    public class ListProfessionalSalaryTypeQueryHandler : IRequestHandler<ListProfessionalSalaryTypeQuery, ListProfessionalSalaryQueryResult>
    {
        private readonly TheFullStackTeamDbContext _context;

        public ListProfessionalSalaryTypeQueryHandler(TheFullStackTeamDbContext context)
        {
            _context = context;
        }

        public async Task<ListProfessionalSalaryQueryResult> Handle(ListProfessionalSalaryTypeQuery request, CancellationToken cancellationToken)
        {
            var professionalSalary = await _context.ProfessionalSalaryTypes.Where(sv => sv.ProfessionalId == request.ProfessionalId).Select(ProfessionalSalaryTypeListItem.Projection).ToListAsync(cancellationToken);
            return new ListProfessionalSalaryQueryResult(professionalSalary);
        }
    }
}
