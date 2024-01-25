using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Skills.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteSkillCommand : IRequest<DeleteSkillCommandResult>
{
    public Guid Id{ get; }

    public DeleteSkillCommand(Guid id)
    {
        Id= id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, DeleteSkillCommandResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public DeleteSkillCommandHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteSkillCommandResult> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Skills.SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Skill), request.Id);
        }

        _context.Skills.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteSkillCommandResult(true);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class DeleteSkillCommandResult : AppResult<bool>
{
    public DeleteSkillCommandResult(bool success) : base(success)
    {
    }
}

/// <summary>
/// Fluent delete skill command validator
/// </summary>
public sealed class DeleteSkillCommandValidator : AbstractValidator<DeleteSkillCommand>
{
    public DeleteSkillCommandValidator(TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Id).Must(id => context.Skills.Any(a => a.Id == id))
            .WithMessage(m => $"Not found entity with this moniker: {m.Id}");
    }
}