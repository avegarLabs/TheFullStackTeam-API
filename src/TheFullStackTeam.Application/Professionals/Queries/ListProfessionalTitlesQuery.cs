using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// A professional titles list query 
/// </summary>
public class ListProfessionalTitlesQuery : IRequest<ListProfessionalTitlesQueryResult>
{
    public Guid ProfessionalId { get; }

    public ListProfessionalTitlesQuery(Guid id)
    {
        ProfessionalId = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class ListProfessionalTitlesQueryHandler : IRequestHandler<ListProfessionalTitlesQuery, ListProfessionalTitlesQueryResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public ListProfessionalTitlesQueryHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<ListProfessionalTitlesQueryResult> Handle(ListProfessionalTitlesQuery request, CancellationToken cancellationToken)
    {
        var professionalTitles = await _context.Titles
            .Where(ps => ps.ProfessionalId == request.ProfessionalId)
            .Select(TitleListItem.Projection).AsNoTracking()
            .ToListAsync(cancellationToken);

        return new ListProfessionalTitlesQueryResult(professionalTitles);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class ListProfessionalTitlesQueryResult : AppResult<IEnumerable<TitleListItem>>
{
    public ListProfessionalTitlesQueryResult(IEnumerable<TitleListItem> model) : base(model)
    {
    }
}


