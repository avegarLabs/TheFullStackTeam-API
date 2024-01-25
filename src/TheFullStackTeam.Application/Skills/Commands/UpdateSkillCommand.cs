using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Model.PUT;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Skills.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateSkillCommand : IRequest<UpdateSkillCommandResult>
{
    public UpdateSkillCommand(Guid id, SkillListItem model)
    {
        Id = id;
        Model = model;
    }

    public Guid Id { get; }
    public SkillListItem Model { get; }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, UpdateSkillCommandResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public UpdateSkillCommandHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateSkillCommandResult> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Skills
            .Include(i => i.SkillCategories)
            .SingleOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Skill), request.Id);
        }

        entity.Name = request.Model.Name;
        entity.SkillCategories = request.Model.Categories.Select(s => new SkillCategory { CategoryId = s }).ToList();

        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateSkillCommandResult(entity);
    }
}

/// <summary>
/// Result class
/// </summary>
public class UpdateSkillCommandResult : AppResult<SkillListItem>
{
    public UpdateSkillCommandResult(SkillListItem model) : base(model)
    {
    }
}

/// <summary>
/// Update validator
/// </summary>
public sealed class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillCommandValidator(TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Model.Moniker).Must(moniker => context.Skills.Any(a => a.Moniker == moniker))
            .WithMessage(m => $"Not found entity with this moniker: {m.Model.Moniker}");

        RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(Skill.NameMaxLenght);
    }
}