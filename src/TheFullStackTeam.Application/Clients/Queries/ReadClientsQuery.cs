using MediatR;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;
using WindowsAzure.Table.Extensions;

namespace TheFullStackTeam.Application.Clients.Queries;

/// <summary>
/// Read all clients query
/// </summary>
public class ReadClientsQuery : IRequest<ReadClientsQueryResult>
{
}

/// <summary>
/// Read clients query handler
/// </summary>
public class ReadClientsQueryHandler : AppRequestHandler, IRequestHandler<ReadClientsQuery, ReadClientsQueryResult>
{
    public ReadClientsQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<ReadClientsQueryResult> Handle(ReadClientsQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Clients.Select(ClientListItem.Projection).ToListAsync(cancellationToken);
        return new ReadClientsQueryResult(response);
    }
}

/// <summary>
/// Read clients query result
/// </summary>
public class ReadClientsQueryResult : AppResult<IEnumerable<ClientListItem>>
{
    public ReadClientsQueryResult(IEnumerable<ClientListItem> data) : base(data)
    {
    }
}