using MediatR;
using TheFullStackTeam.Application.Institution.Results;

namespace TheFullStackTeam.Application.Institution.Queries
{
    public class ListInstitutionQuery: IRequest<InstitutionListQueryResults>
    {
    }
}
