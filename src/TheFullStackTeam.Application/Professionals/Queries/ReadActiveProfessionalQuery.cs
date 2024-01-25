using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

public class ReadActiveProfessionalQuery : IRequest<ActiveProfessionalQueryResult>
{
    public ReadActiveProfessionalQuery(string accountId)
    {
        AccountId = accountId;
    }

    public string AccountId { get; }
}

public class ReadActiveProfessionalQueryHandler : AppRequestHandler, IRequestHandler<ReadActiveProfessionalQuery, ActiveProfessionalQueryResult>
{
    public ReadActiveProfessionalQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<ActiveProfessionalQueryResult> Handle(ReadActiveProfessionalQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Professionals.AsNoTracking()
            .Where(p => p.User.AccountId.Equals(request.AccountId))
            .Select(ProfessionalListItem.Projection)
            .FirstOrDefaultAsync(cancellationToken);

        return new ActiveProfessionalQueryResult(entity);
    }
}

public class ActiveProfessionalQueryResult
{
    public ProfessionalListItem? Data { get; set; }

    public ActiveProfessionalQueryResult(ProfessionalListItem? professionalGet)
    {
        this.Data = professionalGet;
    }
}