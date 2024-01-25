using MediatR;
using TheFullStackTeam.Application.Model.EntityModel.Search;
using TheFullStackTeam.Application.Search.Queries;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Search.Handler
{
    public class SearchProfessionalByParametersQueryHandler : AppRequestHandler, IRequestHandler<SearchProfessionalByParametersQuery, SearchResultProfilesItem>
    {
        private readonly ISearchService _searchService;
        public SearchProfessionalByParametersQueryHandler(TheFullStackTeamDbContext context, ISearchService service) : base(context)
        {
            _searchService = service;
        }

        public async Task<SearchResultProfilesItem> Handle(SearchProfessionalByParametersQuery request, CancellationToken cancellationToken)
        {
            return await _searchService.SearchProfessionalsByCriteria(request.Model, cancellationToken);
        }
    }
}
