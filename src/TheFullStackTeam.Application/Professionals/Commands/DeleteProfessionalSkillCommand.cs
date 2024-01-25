using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteProfessionalSkillCommand : IRequest<DeleteProfessionalSkillCommandResult>
{

    public Guid ProfessionalId { get; set; }
    public Guid Id { get; set; }

    public DeleteProfessionalSkillCommand(Guid pId, Guid id)
    {
        ProfessionalId = pId;
        Id = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class DeleteProfessionalSkillCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalSkillCommand, DeleteProfessionalSkillCommandResult>
{
    public DeleteProfessionalSkillCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<DeleteProfessionalSkillCommandResult> Handle(DeleteProfessionalSkillCommand request, CancellationToken cancellationToken)
    {
        var domainEntity = await _context.ProfessionalSkills
            .Where(ps => ps.Id.Equals(request.Id) && ps.ProfessionalId == request.ProfessionalId)
            .SingleOrDefaultAsync(cancellationToken);

        if (domainEntity == null)
        {
            throw new NotFoundException(nameof(ProfessionalSkill), request.Id);
        }

        _context.ProfessionalSkills.Remove(domainEntity!);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteProfessionalSkillCommandResult(true);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class DeleteProfessionalSkillCommandResult : AppResult<bool>
{
    public DeleteProfessionalSkillCommandResult(bool success) : base(success)
    {
    }
}

/// <summary>
/// Update validator
/// </summary>
public sealed class DeleteProfessionalSkillCommandValidator : AbstractValidator<DeleteProfessionalSkillCommand>
{
    public DeleteProfessionalSkillCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(r => r.Id).Must(m => context.ProfessionalSkills
                .Any(professionalSkill => professionalSkill.Id == m &&
                                          (professionalSkill.Professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");
    }
}