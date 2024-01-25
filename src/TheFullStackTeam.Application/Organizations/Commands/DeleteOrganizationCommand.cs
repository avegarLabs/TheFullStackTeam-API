using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteOrganizationCommand : IRequest<DeleteOrganizationCommandResult>
{
    public Guid Id { get; }

    public DeleteOrganizationCommand(Guid organizationId)
    {
        Id = organizationId;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class DeleteOrganizationHandler : AppRequestHandler, IRequestHandler<DeleteOrganizationCommand, DeleteOrganizationCommandResult>
{
    public DeleteOrganizationHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<DeleteOrganizationCommandResult> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Organizations.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new Exception("Organization not found");
        }
        _context.Organizations.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteOrganizationCommandResult(true);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class DeleteOrganizationCommandResult : AppResult<bool>
{
    public DeleteOrganizationCommandResult(bool success) : base(success)
    {
    }
}

/// <summary>
/// Fluent delete command validator
/// </summary>
public sealed class DeleteOrganizationCommandValidator : AbstractValidator<DeleteOrganizationCommand>
{
    public DeleteOrganizationCommandValidator(TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Id).Must(id => context.Organizations.Any(a => a.Id == id))
            .WithMessage(m => $"Not found entity with this identifier: {m.Id}");
    }
}