using MediatR;
using TheFullStackTeam.Application.Model.EntityModel.Search;

namespace TheFullStackTeam.Application.Search.Queries
{
    public class SearchJobByParametersQuery : IRequest<SearchResultItem>
    {
        public SearchCriteriaModel Model { get; set; }

        public SearchJobByParametersQuery(SearchCriteriaModel queryModel)
        {
            Model = queryModel;
        }
    }
}
