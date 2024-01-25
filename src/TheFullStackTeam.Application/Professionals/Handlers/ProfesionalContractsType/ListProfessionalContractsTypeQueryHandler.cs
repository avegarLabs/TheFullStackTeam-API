using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Queries;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalContractsType
{
    public class ListProfessionalContractsTypeQueryHandler : IRequestHandler<ListProfessionalContractsTypeQuery, ListProfessionalContractTypeCommandResult>
    {
        private readonly TheFullStackTeamDbContext _context;

        public ListProfessionalContractsTypeQueryHandler(TheFullStackTeamDbContext context)
        {
            _context = context;
        }

        public async Task<ListProfessionalContractTypeCommandResult> Handle(ListProfessionalContractsTypeQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.ProfessionalId);
            var contractsTypes = await _context.ProfessionalContractTypes.Where(sv => sv.Professional.Id == request.ProfessionalId).Select(ProfessionalContractTypeListItem.Projection).ToListAsync(cancellationToken);
            Console.WriteLine(contractsTypes.Count());
            return new ListProfessionalContractTypeCommandResult(contractsTypes);
        }
    }
}
