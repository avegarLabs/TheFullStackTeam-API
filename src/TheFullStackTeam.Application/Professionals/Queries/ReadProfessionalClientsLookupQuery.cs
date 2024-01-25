using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Model.GET;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// Read clients lookup query 
/// </summary>
public class ReadProfessionalClientsLookupQuery : IRequest<ReadClientsLookupQueryResult>
{
}

/// <summary>
/// Read clients lookup handler 
/// </summary>
public class ReadClientsLookupQueryHandler : AppRequestHandler, IRequestHandler<ReadProfessionalClientsLookupQuery, ReadClientsLookupQueryResult>
{
    private readonly ISessionService _sessionService;

    public ReadClientsLookupQueryHandler(TheFullStackTeamDbContext context, ISessionService sessionService) : base(context)
    {
        _sessionService = sessionService;
    }

    public async Task<ReadClientsLookupQueryResult> Handle(ReadProfessionalClientsLookupQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Clients.AsNoTracking()
            .Where(p => p.Professional.User.AccountId == _sessionService.AccountId())
            .Select(ClientLookup.Projection)
            .ToListAsync(cancellationToken);

        return new ReadClientsLookupQueryResult(response);
    }
}

/// <summary>
/// Read clients lookup result
/// </summary>
public class ReadClientsLookupQueryResult : AppResult<IEnumerable<ClientLookup>>
{
    public ReadClientsLookupQueryResult(IEnumerable<ClientLookup> data) : base(data)
    {
    }
}