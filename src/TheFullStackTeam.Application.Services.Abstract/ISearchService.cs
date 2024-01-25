using TheFullStackTeam.Application.Model.EntityModel.Search;

namespace TheFullStackTeam.Application.Services.Abstract
{
    public interface ISearchService : IService
    {
        public Task<SearchResultItem> SearchOrganizationAndProfessionalByName(string param, CancellationToken cancellation);

        public Task<SearchResultItem> SearchJobsByCriteria(SearchCriteriaModel criteriaModel, CancellationToken cancellation);

        public Task<SearchResultProfilesItem> SearchProfessionalsByCriteria(SearchCriteriaModel criteriaModel, CancellationToken cancellation);
        public Task<SearchResultServicesItem> SearchServiceCategoriesByCriteria(SearchServiceCriteriaModel criteriaModel, CancellationToken cancellation);
    }
}
