using FluentValidation;
using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Categories.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateCategoryCommand : IRequest<CreateCategoryCommandResult>
{
    public CategoryModel Model { get; set; }

    public CreateCategoryCommand(CategoryModel model)
    {
        Model = model;
    }
}
/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public CreateCategoryCommandHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category entity = request.Model;
        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCategoryCommandResult(entity);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class CreateCategoryCommandResult : AppResult<CategoryListItem>
{
    public CreateCategoryCommandResult(CategoryListItem response) : base(response)
    {
    }
}

/// <summary>
/// Fluent Command validation
/// </summary>
public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Model.Name).NotNull().MaximumLength(Category.NameMaxLenght);
    }
}