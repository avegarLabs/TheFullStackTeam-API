using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.EntityModel.Search;

namespace TheFullStackTeam.Application.Search.Queries;

public class SearchOrganizationServiceParametersQuery : IRequest<SearchResultItem>
{
    public SearchServiceCriteriaModel Model { get; set; }

    public SearchOrganizationServiceParametersQuery(SearchServiceCriteriaModel queryModel)
    {
        Model = queryModel;
    }
}
