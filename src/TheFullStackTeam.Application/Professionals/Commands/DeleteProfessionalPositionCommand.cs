using FluentValidation;
using MediatR;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteProfessionalPositionCommand : IRequest<DeleteProfessionalExperienceCommandResult>
{
    public Guid ProfessionalId { get; set; }
    public Guid PositionId { get; set; }

    public DeleteProfessionalPositionCommand(Guid pId, Guid id)
    {
        ProfessionalId = pId;
        PositionId = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class DeleteProfessionalExperienceCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalPositionCommand, DeleteProfessionalExperienceCommandResult>
{
    public DeleteProfessionalExperienceCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<DeleteProfessionalExperienceCommandResult> Handle(DeleteProfessionalPositionCommand request, CancellationToken cancellationToken)
    {
        var position = _context.Positions
            .Where(p => p.Id == request.PositionId && p.ProfessionalId == request.ProfessionalId)
            .SingleOrDefault();

        if (position == null)
        {
            throw new NotFoundException(nameof(Position), request.PositionId);
        }
        position.SkillPositions.Clear();
        _context.Positions.Remove(position);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteProfessionalExperienceCommandResult(true);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class DeleteProfessionalExperienceCommandResult : AppResult<bool>
{
    public DeleteProfessionalExperienceCommandResult(bool success) : base(success)
    {
    }
}

public sealed class DeleteProfessionalExperienceCommandValidator : AbstractValidator<DeleteProfessionalPositionCommand>
{
    public DeleteProfessionalExperienceCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        /*  RuleFor(cmd => cmd.ProfessionalMoniker).Must(professionalMoniker => context.Professionals
                 .Any(professional => professional.Moniker == professionalMoniker &&
                                      (professional.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
             .WithMessage("You not have a permission to make this operation");*/
    }
}


