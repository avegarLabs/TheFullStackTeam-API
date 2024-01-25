using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Categories.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteCategoryCommand : IRequest<DeleteCategoryCommandResult>
{
    public Guid Id { get; set; }

    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public DeleteCategoryCommandHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteCategoryCommandResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteCategoryCommandResult(true);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class DeleteCategoryCommandResult : AppResult<bool>
{
    public DeleteCategoryCommandResult(bool success) : base(success)
    {
    }
}

/// <summary>
/// Fluent delete command validator
/// </summary>
public sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator(TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Id).Must(id => context.Categories.Any(a => a.Id == id))
            .WithMessage(m => $"Not found entity with this identifier: {m.Id}");
    }
}