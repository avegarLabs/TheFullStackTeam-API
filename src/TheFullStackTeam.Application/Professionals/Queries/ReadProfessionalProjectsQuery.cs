using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

public class ReadProfessionalProjectsQuery : IRequest<ReadProfessionalProjectsQueryResult>
{
    public Guid ProfessionalId { get; set; }
    public ReadProfessionalProjectsQuery(Guid id)
    {
        ProfessionalId = id;
    }


}

public class ReadProfessionalProjectsQueryHandler : AppRequestHandler, IRequestHandler<ReadProfessionalProjectsQuery, ReadProfessionalProjectsQueryResult>
{
    public ReadProfessionalProjectsQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<ReadProfessionalProjectsQueryResult> Handle(ReadProfessionalProjectsQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Projects.AsNoTracking()
            .Where(p => p.ProfessionalId == request.ProfessionalId)
            .Select(ProjectListItem.Projection)
            .ToListAsync(cancellationToken);

        return new ReadProfessionalProjectsQueryResult(response);
    }
}

public class ReadProfessionalProjectsQueryResult : AppResult<IEnumerable<ProjectListItem>>
{
    public ReadProfessionalProjectsQueryResult(IEnumerable<ProjectListItem> data) : base(data)
    {
    }
}