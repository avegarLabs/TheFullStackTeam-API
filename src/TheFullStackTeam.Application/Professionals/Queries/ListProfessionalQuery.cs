using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// A professional list query 
/// </summary>
public class ListProfessionalQuery : IRequest<ListProfessionalQueryResult>
{
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class ListProfessionalQueryHandler : AppRequestHandler, IRequestHandler<ListProfessionalQuery, ListProfessionalQueryResult>
{
    public ListProfessionalQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<ListProfessionalQueryResult> Handle(ListProfessionalQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Professionals
            .AsNoTracking()
            .Select(ProfessionalListItem.Projection)
            .ToListAsync(cancellationToken);

        return new ListProfessionalQueryResult(response);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class ListProfessionalQueryResult : AppResult<IEnumerable<ProfessionalListItem>>
{
    public ListProfessionalQueryResult(IEnumerable<ProfessionalListItem> model) : base(model)
    {
    }
}