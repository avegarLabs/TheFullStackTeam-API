using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

public class ListProfessionalPositionQuery : IRequest<ListProfessionalExperiencesQueryResult>
{
    public Guid ProfessionalId { get; }

    public ListProfessionalPositionQuery(Guid id)
    {
        ProfessionalId = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class ListProfessionalExperiencesQueryHandler : IRequestHandler<ListProfessionalPositionQuery, ListProfessionalExperiencesQueryResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public ListProfessionalExperiencesQueryHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<ListProfessionalExperiencesQueryResult> Handle(ListProfessionalPositionQuery request, CancellationToken cancellationToken)
    {
        var experiences = await _context.Positions
            .Where(ps => ps.ProfessionalId == request.ProfessionalId)
            .Select(PositionListItem.Projection)
            .ToListAsync(cancellationToken);

        return new ListProfessionalExperiencesQueryResult(experiences);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class ListProfessionalExperiencesQueryResult : AppResult<IEnumerable<PositionListItem>>
{
    public ListProfessionalExperiencesQueryResult(IEnumerable<PositionListItem> model) : base(model)
    {
    }
}

