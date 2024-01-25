using MediatR;
using TheFullStackTeam.Application.Model.EntityModel.Search;

namespace TheFullStackTeam.Application.Search.Queries
{
    public class SearchProfessionalByParametersQuery: IRequest<SearchResultProfilesItem>
    {
        public SearchCriteriaModel Model { get; set; }

        public SearchProfessionalByParametersQuery(SearchCriteriaModel queryModel) {
        Model = queryModel;
        }
    }
}
