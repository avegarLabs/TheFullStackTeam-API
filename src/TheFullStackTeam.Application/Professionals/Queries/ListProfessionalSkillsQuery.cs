using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// A professional skills list query 
/// </summary>
public class ListProfessionalSkillsQuery : IRequest<ListProfessionalSkillsQueryResult>
{
    public Guid ProfessionalId { get; }

    public ListProfessionalSkillsQuery(Guid id)
    {
        ProfessionalId = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class ListProfessionalSkillsQueryHandler : IRequestHandler<ListProfessionalSkillsQuery, ListProfessionalSkillsQueryResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public ListProfessionalSkillsQueryHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<ListProfessionalSkillsQueryResult> Handle(ListProfessionalSkillsQuery request, CancellationToken cancellationToken)
    {
        var professionalSkills = await _context.ProfessionalSkills
            .Where(ps => ps.ProfessionalId == request.ProfessionalId)
            .Select(ProfessionalSkillListItem.Projection)
            .ToListAsync(cancellationToken);

        return new ListProfessionalSkillsQueryResult(professionalSkills);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class ListProfessionalSkillsQueryResult : AppResult<IEnumerable<ProfessionalSkillListItem>>
{
    public ListProfessionalSkillsQueryResult(IEnumerable<ProfessionalSkillListItem> model) : base(model)
    {
    }
}