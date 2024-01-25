using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalProjects;

public class UpdateProfessionalProjectCommand : IRequest<UpdateProjectCommandResult>
{
    public Guid ProfessionalId { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectModel Model { get; set; }

    public UpdateProfessionalProjectCommand(Guid pId, Guid pjId, ProjectModel model)
    {
        ProfessionalId = pId;
        ProjectId = pjId;
        Model = model;
    }


}

public class UpdateProjectCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalProjectCommand, UpdateProjectCommandResult>
{
    public UpdateProjectCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<UpdateProjectCommandResult> Handle(UpdateProfessionalProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects.AsNoTracking().Where(p => p.Id == request.ProjectId && p.ProfessionalId == request.ProfessionalId)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        var client = await _context.Clients.AsNoTracking().Where(c => c.Id == request.Model.ClientId)
            .SingleOrDefaultAsync(cancellationToken);
        if (client == null)
        {
            throw new NotFoundException(nameof(Clients), request.Model.ClientId);
        }

        entity.Name = request.Model.Name;
        entity.Description = request.Model.Description;
        entity.ClientId = client.Id;

        _context.Projects.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateProjectCommandResult(entity);
    }
}

public class UpdateProjectCommandResult : AppResult<ProjectListItem>
{
    public UpdateProjectCommandResult(ProjectListItem data) : base(data)
    {
    }
}

public sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProfessionalProjectCommand>
{
    public UpdateProjectCommandValidator(ISessionService sessionService, TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Model.Description).NotEmpty().MaximumLength(Project.DescriptionMaxLength);
        RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(Project.NameMaxLength);

        RuleFor(x => x.ProfessionalId).NotEmpty().Must(m =>
                context.Projects.Any(a => a.Id == m
                                          && (a.Professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage("Professional not found or you not have permission to access it.");
    }
}