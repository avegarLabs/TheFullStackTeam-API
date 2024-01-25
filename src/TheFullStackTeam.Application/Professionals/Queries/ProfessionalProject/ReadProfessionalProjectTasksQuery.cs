using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Model.GET;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries.ProfessionalProject;

public class ReadProfessionalProjectTasksQuery : IRequest<ReadProjectTasksQueryResult>
{
    public ReadProfessionalProjectTasksQuery(string projectMoniker)
    {
        ProjectMoniker = projectMoniker;
    }

    public string ProjectMoniker { get; set; }
}

public class ReadProjectTasksQueryHandler : AppRequestHandler, IRequestHandler<ReadProfessionalProjectTasksQuery, ReadProjectTasksQueryResult>
{
    private readonly ISessionService _sessionService;

    public ReadProjectTasksQueryHandler(TheFullStackTeamDbContext context, ISessionService sessionService) : base(context)
    {
        _sessionService = sessionService;
    }

    public async Task<ReadProjectTasksQueryResult> Handle(ReadProfessionalProjectTasksQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.ProjectTasks.AsNoTracking()
            .Where(p => p.Project.Moniker == request.ProjectMoniker
                        && (p.Project.Professional.User.AccountId == _sessionService.AccountId() || _sessionService.IsAdmin()))
            .Select(ProjectTaskGet.Projection)
            .ToListAsync(cancellationToken);
        return new ReadProjectTasksQueryResult(response);
    }
}

public class ReadProjectTasksQueryResult : AppResult<IEnumerable<ProjectTaskGet>>
{
    public ReadProjectTasksQueryResult(IEnumerable<ProjectTaskGet> data) : base(data)
    {
    }
}