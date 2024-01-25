using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalProjects;

public class CreateProfessionalProjectCommand : IRequest<CreateProjectCommandResult>
{
    public ProjectModel Model { get; set; }
    public Guid ProfessionalId { get; set; }

    public CreateProfessionalProjectCommand(ProjectModel model, Guid id)
    {
        Model = model;
        ProfessionalId = id;
    }

}

public class CreateProjectCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalProjectCommand, CreateProjectCommandResult>
{
    private readonly IMonikerService _monikerService;
    private readonly ISessionService _sessionService;

    public CreateProjectCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService, ISessionService sessionService) : base(context)
    {
        _monikerService = monikerService;
        _sessionService = sessionService;
    }

    public async Task<CreateProjectCommandResult> Handle(CreateProfessionalProjectCommand request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals.AsNoTracking()
            .Where(p => p.Id == request.ProfessionalId)
            .SingleOrDefaultAsync(cancellationToken);

        if (professional == null)
        {
            throw new NotFoundException(nameof(professional), request.ProfessionalId);
        }

        var client = await _context.Clients.AsNoTracking()
            .Where(c => c.Id == request.Model.ClientId)
            .SingleOrDefaultAsync(cancellationToken);

        if (client == null)
        {
            throw new Exception("Client not found");
        }

        Project entity = request.Model;
        entity.ProfessionalId = professional.Id;
        entity.ClientId = client.Id;
        entity.Moniker = await _monikerService.FindValidMoniker<Project>(entity.Name);

        await _context.Projects.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProjectCommandResult(entity);
    }
}

public class CreateProjectCommandResult : AppResult<ProjectListItem>
{
    public CreateProjectCommandResult(ProjectListItem model) : base(model)
    {
    }
}

public sealed class CreateCommandValidator : AbstractValidator<CreateProfessionalProjectCommand>
{
    public CreateCommandValidator(ISessionService sessionService, TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Model.Description).NotEmpty().MaximumLength(Project.DescriptionMaxLength);
        RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(Project.NameMaxLength);

        RuleFor(x => x.Model.ClientId).NotEmpty().Must(m => context.Clients.Any(a => a.Id == m))
            .WithMessage("Client not found");

        RuleFor(x => x.ProfessionalId).NotEmpty().Must(m => context.Professionals.Any(a => a.Id == m))
            .WithMessage("Professional not found");
    }
}