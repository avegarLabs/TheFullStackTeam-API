using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Model.PUT;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Categories.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateCategoryCommand : IRequest<UpdateCategoryCommandResult>
{
    public UpdateCategoryCommand(Guid id, CategoryPut model)
    {
        Id = id;
        Model = model;
    }

    public Guid Id { get; }
    public CategoryPut Model { get; }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public UpdateCategoryCommandHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateCategoryCommandResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        entity.Name = request.Model.Name;
        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryCommandResult(entity);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class UpdateCategoryCommandResult : AppResult<CategoryListItem>
{
    public UpdateCategoryCommandResult(CategoryListItem model) : base(model)
    {
    }
}

/// <summary>
/// Fluent update country command validataor 
/// </summary>
public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator(TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Id).Must(id => context.Categories.Any(a => a.Id == id))
            .WithMessage(m => $"Not found entity with this identifier: {m.Id}");

        RuleFor(x => x.Model.Name).MaximumLength(Category.NameMaxLenght);

    }
}