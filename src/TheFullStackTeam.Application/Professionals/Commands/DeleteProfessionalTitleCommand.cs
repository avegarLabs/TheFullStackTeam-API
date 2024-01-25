using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteProfessionalTitleCommand : IRequest<DeleteProfessionalTitleCommandResult>
{
    public Guid ProfessionalId { get; set; }
    public Guid TitleId { get; set; }

    public DeleteProfessionalTitleCommand(Guid pId, Guid id)
    {
        ProfessionalId = pId;
        TitleId = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class DeleteProfessionalTitleCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalTitleCommand, DeleteProfessionalTitleCommandResult>
{
    public DeleteProfessionalTitleCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<DeleteProfessionalTitleCommandResult> Handle(DeleteProfessionalTitleCommand request, CancellationToken cancellationToken)
    {
        var title = await _context.Titles
            .Where(t => t.Id == request.TitleId && t.ProfessionalId == request.ProfessionalId)
            .SingleOrDefaultAsync(cancellationToken);

        if (title == null)
        {
            throw new NotFoundException(nameof(Title), request.TitleId);
        }

        _context.Titles.Remove(title);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteProfessionalTitleCommandResult(true);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class DeleteProfessionalTitleCommandResult : AppResult<bool>
{
    public DeleteProfessionalTitleCommandResult(bool success) : base(success)
    {
    }
}

public sealed class DeleteProfessionalTitleCommandValidator : AbstractValidator<DeleteProfessionalTitleCommand>
{
    public DeleteProfessionalTitleCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(cmd => cmd.TitleId).Must(id => context.Titles.Any(p => p.Id == id))
            .WithMessage("Title does not exist");

        /*RuleFor(cmd => cmd.ProfessionalMoniker).Must(professionalMoniker => context.Professionals
                .Any(professional => professional.Moniker == professionalMoniker &&
                                     (professional.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage("You not have a permission to make this operation");*/
    }
}