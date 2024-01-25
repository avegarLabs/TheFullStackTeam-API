using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteProfessionalCommand : IRequest<DeleteProfessionalCommandResult>
{
    public Guid ProfessionalId { get; }

    public DeleteProfessionalCommand(Guid id)
    {
        ProfessionalId = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class DeleteUserProfileCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalCommand, DeleteProfessionalCommandResult>
{

    public DeleteUserProfileCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<DeleteProfessionalCommandResult> Handle(DeleteProfessionalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Professionals.SingleOrDefaultAsync(p => p.Id == request.ProfessionalId, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Professional), request.ProfessionalId);
        }

        await ClearProfessionalRalationShip(entity, cancellationToken);

        _context.Professionals.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteProfessionalCommandResult(true);
    }

    private async Task<Task> ClearProfessionalRalationShip(Professional entity, CancellationToken cancellationToken)
    {
        var skills = await _context.ProfessionalSkills.Where(ps => ps.ProfessionalId == entity.Id).AsNoTracking().ToListAsync(cancellationToken);
        _context.ProfessionalSkills.RemoveRange(skills);

        var services = await _context.ProfessionalSevices.Where(ps => ps.ProfessionalId == entity.Id).AsNoTracking().ToListAsync(cancellationToken);
        _context.ProfessionalSevices.RemoveRange(services);

        var titles = await _context.Titles.Where(ps => ps.ProfessionalId == entity.Id).AsNoTracking().ToListAsync(cancellationToken);
        _context.Titles.RemoveRange(titles);

        var positions = await _context.Positions.Where(ps => ps.ProfessionalId == entity.Id).AsNoTracking().ToListAsync(cancellationToken);
        _context.Positions.RemoveRange(positions);

        var salaryTypes = await _context.ProfessionalSalaryTypes.Where(ps => ps.ProfessionalId == entity.Id).AsNoTracking().ToListAsync(cancellationToken);
        _context.ProfessionalSalaryTypes.RemoveRange(salaryTypes);

        var languages = await _context.ProfessionalLanguages.Where(ps => ps.ProfessionalId == entity.Id).AsNoTracking().ToListAsync(cancellationToken);
        _context.ProfessionalLanguages.RemoveRange(languages);

        var contractTypes = await _context.ProfessionalContractTypes.Where(ps => ps.ProfessionalId == entity.Id).AsNoTracking().ToListAsync(cancellationToken);
        _context.ProfessionalContractTypes.RemoveRange(contractTypes);

        var jobTypes = await _context.ProfessionalJobTypes.Where(ps => ps.ProfessionalId == entity.Id).AsNoTracking().ToListAsync(cancellationToken);
        _context.ProfessionalJobTypes.RemoveRange(jobTypes);

        await _context.SaveChangesAsync(cancellationToken);
        return Task.CompletedTask;
       
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class DeleteProfessionalCommandResult : AppResult<bool>
{
    public DeleteProfessionalCommandResult(bool success) : base(success)
    {
    }
}

/// <summary>
/// Fluent delete command validator
/// </summary>
public sealed class DeleteProfessionalCommandValidator : AbstractValidator<DeleteProfessionalCommand>
{
    public DeleteProfessionalCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(x => x.ProfessionalId).Must(moniker => context.Professionals.Any(a => a.Id == moniker))
            .WithMessage(m => $"Not found entity with this moniker: {m.ProfessionalId}");

        RuleFor(r => r.ProfessionalId).Must(m => context.Professionals
         .Any(professional => professional.Id == m &&
                              (professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
     .WithMessage(_ => "You not have a permission to make this operation");
    }
}


