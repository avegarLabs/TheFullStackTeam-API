using MediatR;
using TheFullStackTeam.Application.Model.EntityModel.Search;
using TheFullStackTeam.Application.Search.Queries;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Search.Handler
{
    public class SearchJobByParametersQueryHandler : AppRequestHandler, IRequestHandler<SearchJobByParametersQuery, SearchResultItem>
    {
        private readonly ISearchService _service;
        public SearchJobByParametersQueryHandler(TheFullStackTeamDbContext context, ISearchService service) : base(context)
        {
            _service = service;
        }

        public async Task<SearchResultItem> Handle(SearchJobByParametersQuery request, CancellationToken cancellationToken)
        {
            return await _service.SearchJobsByCriteria(request.Model, cancellationToken);
        }
    }
}
