using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalProjects;

public class DeleteProfessionalProjectCommand : IRequest<DeleteProjectCommandResult>
{
    public Guid ProjectId { get; set; }

    public DeleteProfessionalProjectCommand(Guid pjId)
    {
        ProjectId = pjId;

    }

}

public class DeleteProjectCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalProjectCommand, DeleteProjectCommandResult>
{
    private readonly IMonikerService _monikerService;
    private readonly ISessionService _sessionService;

    public DeleteProjectCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService, ISessionService sessionService) : base(context)
    {
        _monikerService = monikerService;
        _sessionService = sessionService;
    }

    public async Task<DeleteProjectCommandResult> Handle(DeleteProfessionalProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.Where(pj => pj.Id == request.ProjectId).AsNoTracking().SingleOrDefaultAsync(cancellationToken);

        if (project == null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteProjectCommandResult(true);
    }
}

public class DeleteProjectCommandResult : AppResult<bool>
{
    public DeleteProjectCommandResult(bool success) : base(success)
    {
    }
}

public sealed class DeleteCommandValidator : AbstractValidator<DeleteProfessionalProjectCommand>
{
    public DeleteCommandValidator(TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.ProjectId).NotEmpty().Must(m => context.Projects.Any(a => a.Id == m))
            .WithMessage("Project not found");
    }
}