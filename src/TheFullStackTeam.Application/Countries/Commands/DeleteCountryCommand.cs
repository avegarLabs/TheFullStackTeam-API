using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Countries.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteCountryCommand : IRequest<DeleteCountryCommandResult>
{
    public Guid Id { get; set; }

    public DeleteCountryCommand(Guid id)
    {
        Id = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, DeleteCountryCommandResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public DeleteCountryCommandHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteCountryCommandResult> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Countries.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Country), request.Id);
        }

        _context.Countries.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteCountryCommandResult(true);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class DeleteCountryCommandResult : AppResult<bool>
{
    public DeleteCountryCommandResult(bool success) : base(success)
    {
    }
}

/// <summary>
/// Fluent delete command validator
/// </summary>
public sealed class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
{
    public DeleteCountryCommandValidator(TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Id).Must(id => context.Countries.Any(a => a.Id == id))
            .WithMessage(m => $"Not found entity with this identifier: {m.Id}");
    }
}