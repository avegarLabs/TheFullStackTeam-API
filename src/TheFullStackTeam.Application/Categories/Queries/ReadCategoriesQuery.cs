using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Categories.Queries;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class ReadCategoriesQuery : IRequest<ReadCategoriesQueryResult>
{
}
/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class ReadCategoriesQueryHandler : IRequestHandler<ReadCategoriesQuery, ReadCategoriesQueryResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public ReadCategoriesQueryHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<ReadCategoriesQueryResult> Handle(ReadCategoriesQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Categories.Select(CategoryListItem.Projection).ToListAsync(cancellationToken);
        return new ReadCategoriesQueryResult(response);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class ReadCategoriesQueryResult : AppResult<IEnumerable<CategoryListItem>>
{
    public ReadCategoriesQueryResult(IEnumerable<CategoryListItem> model) : base(model)
    {
    }
}