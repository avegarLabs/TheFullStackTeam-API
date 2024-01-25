using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteProfessionalHonorCommand : IRequest<DeleteProfessionalHonorCommandResult>
{
    public Guid HonorId { get; set; }

    public DeleteProfessionalHonorCommand(Guid id)
    {
        HonorId = id;

    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class DeleteProfessionalHonorCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalHonorCommand, DeleteProfessionalHonorCommandResult>
{
    public DeleteProfessionalHonorCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<DeleteProfessionalHonorCommandResult> Handle(DeleteProfessionalHonorCommand request, CancellationToken cancellationToken)
    {
        var honor = await _context.Honors
            .Where(h => h.Id == request.HonorId)
            .SingleOrDefaultAsync(cancellationToken);

        if (honor == null)
        {
            throw new NotFoundException(nameof(Honor), request.HonorId);
        }

        _context.Honors.Remove(honor);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteProfessionalHonorCommandResult(true);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class DeleteProfessionalHonorCommandResult : AppResult<bool>
{
    public DeleteProfessionalHonorCommandResult(bool success) : base(success)
    {
    }
}

public sealed class DeleteProfessionalHonorCommandValidator : AbstractValidator<DeleteProfessionalHonorCommand>
{
    public DeleteProfessionalHonorCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {

        /*RuleFor(cmd => cmd.).Must(professionalMoniker => context.Professionals
                .Any(professional => professional.Moniker == professionalMoniker &&
                                     (professional.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage("You not have a permission to make this operation");*/
    }
}