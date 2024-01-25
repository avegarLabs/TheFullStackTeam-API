using MediatR;
using TheFullStackTeam.Application.Model.EntityModel.Search;

namespace TheFullStackTeam.Application.Search.Queries
{
    public class SearchServicesByParametersQuery : IRequest<SearchResultServicesItem>
    {
        public SearchServiceCriteriaModel Model { get; set; }

        public SearchServicesByParametersQuery(SearchServiceCriteriaModel queryModel)
        {
            Model = queryModel;
        }
    }
}
