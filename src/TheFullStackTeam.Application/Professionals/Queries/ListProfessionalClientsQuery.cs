using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// List a professional clients
/// </summary>
public class ListProfessionalClientsQuery : IRequest<ListProfessionalClientsQueryResult>
{
    public Guid ProfessionalId { get; set; }
    public ListProfessionalClientsQuery(Guid id)
    {
        ProfessionalId = id;
    }


}

/// <summary>
/// List professional clients command result
/// </summary>
public class ListProfessionalClientsQueryHandler : AppRequestHandler, IRequestHandler<ListProfessionalClientsQuery, ListProfessionalClientsQueryResult>
{
    public ListProfessionalClientsQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<ListProfessionalClientsQueryResult> Handle(ListProfessionalClientsQuery request, CancellationToken cancellationToken)
    {
        var professionalClients = await _context.Clients
            .Where(p => p.ProfessionalId == request.ProfessionalId)
            .Select(ClientListItem.Projection)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ListProfessionalClientsQueryResult(professionalClients);
    }
}

/// <summary>
/// Professional clients result
/// </summary>
public class ListProfessionalClientsQueryResult : AppResult<IEnumerable<ClientListItem>>
{
    public ListProfessionalClientsQueryResult(IEnumerable<ClientListItem> model) : base(model)
    {
    }
}