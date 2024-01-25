using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.GET;
using TheFullStackTeam.Application.Model.POST;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalProjects;

/// <summary>
/// TODO: Add a description of the class here
/// </summary>
public class CreateProfessionalProjectTaskCommand : IRequest<CreateProjectTaskCommandResult>
{
    public CreateProfessionalProjectTaskCommand(string projectMoniker, ProjectTaskPost model)
    {
        ProjectMoniker = projectMoniker;
        Model = model;
    }

    public string ProjectMoniker { get; set; }
    public ProjectTaskPost Model { get; set; }
}

/// <summary>
/// TODO:Add a description of the class here
/// </summary>
public class CreateProjectTaskCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalProjectTaskCommand, CreateProjectTaskCommandResult>
{
    private readonly IMonikerService _monikerService;

    public CreateProjectTaskCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService) : base(context)
    {
        _monikerService = monikerService;
    }

    public async Task<CreateProjectTaskCommandResult> Handle(CreateProfessionalProjectTaskCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.AsNoTracking().Where(p => p.Moniker == request.ProjectMoniker).SingleOrDefaultAsync(cancellationToken);
        if (project == null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectMoniker);
        }

        ProjectTask entity = request.Model;
        entity.ProjectId = project.Id;
        entity.Moniker = await _monikerService.FindValidMoniker<ProjectTask>(request.Model.Name);

        await _context.ProjectTasks.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        ProjectTaskGet result = entity;
        result.ProjectName = project.Name;
        result.ProjectMoniker = project.Moniker;

        return new CreateProjectTaskCommandResult(result);
    }
}

/// <summary>
/// TODO: Add summary here
/// </summary>
public class CreateProjectTaskCommandResult : AppResult<ProjectTaskGet>
{
    public CreateProjectTaskCommandResult(ProjectTaskGet data) : base(data)
    {
    }
}

/// <summary>
/// TODO: Add summary here
/// </summary>
public sealed class CreateProjectTaskCommandValidator : AbstractValidator<CreateProfessionalProjectTaskCommand>
{
    public CreateProjectTaskCommandValidator(ISessionService sessionService, TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Model.Description).NotEmpty().MaximumLength(ProjectTask.DescriptionMaxLength);
        RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(ProjectTask.NameMaxLength);

        RuleFor(x => x.ProjectMoniker).NotEmpty()
            .Must(m => context.Projects.Any(a => a.Moniker == m
                                                 && (a.Professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage("Project not found or you do not have access to it.");
    }
}