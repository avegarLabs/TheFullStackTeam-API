using MediatR;
using TheFullStackTeam.Application.Model.EntityModel.Search;

namespace TheFullStackTeam.Application.Search.Queries
{
    public class SearchByNameQuery: IRequest<SearchResultItem>
    {
        public string Name { get; set; }

        public SearchByNameQuery(string param) {
          Name = param;
        }
    }
}
