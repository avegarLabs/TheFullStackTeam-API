using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.EntityModel.Search;
using TheFullStackTeam.Application.Search.Queries;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Search.Handler
{
    public class SearchServiceByParametersQueryHandler : AppRequestHandler, IRequestHandler<SearchServicesByParametersQuery, SearchResultServicesItem>
    {
        private readonly ISearchService _searchService;
        public SearchServiceByParametersQueryHandler(TheFullStackTeamDbContext context, ISearchService service) : base(context)
        {
            _searchService = service;
        }

        public async Task<SearchResultServicesItem> Handle(SearchServicesByParametersQuery request, CancellationToken cancellationToken)
        {
            return await _searchService.SearchServiceCategoriesByCriteria(request.Model, cancellationToken);
        }
    }
}
