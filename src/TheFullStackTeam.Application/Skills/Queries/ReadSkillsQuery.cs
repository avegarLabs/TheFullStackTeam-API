using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Skills.Queries;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class ReadSkillsQuery : IRequest<SkillsQueryResult>
{
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class SkillsQueryHandler : IRequestHandler<ReadSkillsQuery, SkillsQueryResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public SkillsQueryHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }
    public async Task<SkillsQueryResult> Handle(ReadSkillsQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Skills.Select(SkillListItem.Projection).ToListAsync(cancellationToken);
        return new SkillsQueryResult(response);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class SkillsQueryResult : AppResult<IEnumerable<SkillListItem>>
{
    public SkillsQueryResult(IEnumerable<SkillListItem> model) : base(model)
    {
    }
}