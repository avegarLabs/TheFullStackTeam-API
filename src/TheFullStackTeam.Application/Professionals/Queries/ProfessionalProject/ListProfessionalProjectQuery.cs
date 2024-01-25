using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries.ProfessionalProject;

public class ListProfessionalProjectQuery : IRequest<ListProjectQueryResult>
{
    public Guid ProfessionalId { get; set; }

    public ListProfessionalProjectQuery(Guid professionalId)
    {
        ProfessionalId = professionalId;
    }
}

public class ListProjectQueryHandler : AppRequestHandler, IRequestHandler<ListProfessionalProjectQuery, ListProjectQueryResult>
{
    public ListProjectQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {

    }

    public async Task<ListProjectQueryResult> Handle(ListProfessionalProjectQuery request, CancellationToken cancellationToken)
    {
        var projects = await _context.Projects
            .Where(ps => ps.ProfessionalId == request.ProfessionalId)
            .Select(ProjectListItem.Projection)
            .ToListAsync(cancellationToken);

        return new ListProjectQueryResult(projects);
    }
}

public class ListProjectQueryResult : AppResult<IEnumerable<ProjectListItem>>
{
    public ListProjectQueryResult(IEnumerable<ProjectListItem> data) : base(data)
    {
    }
}