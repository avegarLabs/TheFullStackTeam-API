using MediatR;
using TheFullStackTeam.Application.Model.EntityModel.Search;
using TheFullStackTeam.Application.Search.Queries;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Search.Handler;

public class SearchOrganizationServiceByParametersQueryHandler : AppRequestHandler, IRequestHandler<SearchOrganizationServiceParametersQuery, SearchResultItem>
{
    private readonly ISearchService _searchService;
    public SearchOrganizationServiceByParametersQueryHandler(TheFullStackTeamDbContext context, ISearchService service) : base(context)
    {
        _searchService = service;
    }

    public async Task<SearchResultItem> Handle(SearchOrganizationServiceParametersQuery request, CancellationToken cancellationToken)
    {
        return await _searchService.SearchOrganizationServiceByCriteria(request.Model, cancellationToken);
    }
}
