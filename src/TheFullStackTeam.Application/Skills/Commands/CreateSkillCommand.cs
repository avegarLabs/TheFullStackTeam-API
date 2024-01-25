using FluentValidation;
using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Skills.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateSkillCommand : IRequest<CreateSkillCommandResult>
{
    public SkillModel Model { get; set; }

    public CreateSkillCommand(SkillModel model )
    {
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CreateSkillCommandHandler : AppRequestHandler, IRequestHandler<CreateSkillCommand, CreateSkillCommandResult>
{
    private readonly IMonikerService _monikerService;

    public CreateSkillCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService) : base(context)
    {
        _monikerService = monikerService;
    }

    public async Task<CreateSkillCommandResult> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var moniker = await _monikerService.FindValidMoniker<Skill>(request.Model.Name.Trim());

        Skill entity = request.Model;
        entity.Moniker = moniker;

        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateSkillCommandResult(entity);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class CreateSkillCommandResult : AppResult<SkillListItem>
{
    public CreateSkillCommandResult(SkillListItem response) : base(response)
    {
    }
}

/// <summary>
/// Fluent create skill command validator
/// </summary>
public sealed class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
    {
        RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(Skill.NameMaxLenght);
    }
}