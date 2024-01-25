using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.EntityModel.Search;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Search.Queries;
using TheFullStackTeam.Application.Search.Results;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Search.Handler
{
    public class SearchByNameQueryHandler : AppRequestHandler, IRequestHandler<SearchByNameQuery, SearchResultItem>
    {
        private readonly ISearchService _searchService;
        public SearchByNameQueryHandler(TheFullStackTeamDbContext context, ISearchService service) : base(context)
        {
            _searchService = service;
        }

        public async Task<SearchResultItem> Handle(SearchByNameQuery request, CancellationToken cancellationToken)
        {
            return await _searchService.SearchOrganizationAndProfessionalByName(request.Name, cancellationToken);
        }
    }
}
