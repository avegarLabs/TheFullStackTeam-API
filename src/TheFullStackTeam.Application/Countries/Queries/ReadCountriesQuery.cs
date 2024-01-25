using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Countries.Queries;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class ReadCountriesQuery : IRequest<CountriesQueryResult>
{
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CountriesQueryHandler : IRequestHandler<ReadCountriesQuery, CountriesQueryResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public CountriesQueryHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<CountriesQueryResult> Handle(ReadCountriesQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Countries.Select(CountryListItem.Projection).ToListAsync(cancellationToken);
        return new CountriesQueryResult(response);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class CountriesQueryResult : AppResult<IEnumerable<CountryListItem>>
{
    public CountriesQueryResult(IEnumerable<CountryListItem> model) : base(model)
    {
    }
}