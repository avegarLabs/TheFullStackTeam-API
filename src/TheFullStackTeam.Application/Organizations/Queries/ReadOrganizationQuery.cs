using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Queries;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class ReadOrganizationsQuery : IRequest<OrganizationQueryResult>
{
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class OrganizationQueryHandler : IRequestHandler<ReadOrganizationsQuery, OrganizationQueryResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public OrganizationQueryHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<OrganizationQueryResult> Handle(ReadOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var response =  await _context.Organizations
            .AsNoTracking().Select(OrganizationListItem.Projection).ToListAsync(cancellationToken);
     
        return new OrganizationQueryResult(response);
    }
}
/// <inheritdoc cref="AppResult{TModel}"/>
public class OrganizationQueryResult : AppResult<IEnumerable<OrganizationListItem>>
{
    public OrganizationQueryResult(IEnumerable<OrganizationListItem> model) : base(model)
    {
    }
}