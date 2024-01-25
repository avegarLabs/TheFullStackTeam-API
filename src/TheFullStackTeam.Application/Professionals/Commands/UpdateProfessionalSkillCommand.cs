using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateProfessionalSkillCommand : IRequest<UpdateProfessionalSkillResult>
{
    public Guid ProfessionalId { get; }
    public Guid ProfessionalSkillId { get; }
    public ProfessionalSkillModel Model { get; set; }

    public UpdateProfessionalSkillCommand(ProfessionalSkillModel model, Guid id, Guid idPs)
    {
        ProfessionalId = id;
        ProfessionalSkillId = idPs;
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class UpdateProfessionalSkillCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalSkillCommand, UpdateProfessionalSkillResult>
{
    public readonly IMonikerService _monikerService;
    public readonly ILogger<UpdateProfessionalSkillCommandHandler> _logger;

    public UpdateProfessionalSkillCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService, ILogger<UpdateProfessionalSkillCommandHandler> logger) : base(context)
    {
        _monikerService = monikerService;
        _logger = logger;
    }

    public async Task<UpdateProfessionalSkillResult> Handle(UpdateProfessionalSkillCommand request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
        if (professional == null)
        {
            throw new NotFoundException(nameof(Professional), request.ProfessionalId);
        }

        var domainEntity = await _context.ProfessionalSkills.AsNoTracking()
            .Where(ps => ps.Id == request.ProfessionalSkillId && ps.ProfessionalId == request.ProfessionalId)
            .SingleOrDefaultAsync(cancellationToken);

        if (domainEntity == null)
        {
            throw new NotFoundException(nameof(ProfessionalSkill), request.Model.SkillMoniker);
        }

        var skill = await _context.Skills.Where(k => k.Moniker == request.Model.SkillMoniker)
            .SingleOrDefaultAsync(cancellationToken);

        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            if (skill == null)
            {
                skill = new Skill
                {
                    Name = request.Model.Name,
                    Moniker = await _monikerService.FindValidMoniker<Skill>(request.Model.Name)
                };
                await _context.Skills.AddAsync(skill, cancellationToken);
            }
            domainEntity.SkillLevel = request.Model.Level;
            domainEntity.SkillName = request.Model.Name;
            domainEntity.SkillId = skill.Id;

            _context.ProfessionalSkills.Update(domainEntity);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return new UpdateProfessionalSkillResult(domainEntity!);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "ErrorEventArgs on update Skil");
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}

/// <summary>
/// Result class
/// </summary>
public class UpdateProfessionalSkillResult : AppResult<ProfessionalSkillListItem>
{
    public UpdateProfessionalSkillResult(ProfessionalSkillListItem model) : base(model)
    {
    }
}

public sealed class UpdateProfessionalSkillCommandValidator : AbstractValidator<UpdateProfessionalSkillCommand>
{
    public UpdateProfessionalSkillCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(cmd => cmd.ProfessionalId).Must(m => context.Professionals
                .Any(professional => professional.Id == m || sessionService.IsAdmin()))
            .WithMessage(_ => "You not have a permission to make this operation");

        RuleFor(cmd => cmd).Must(m => context.ProfessionalSkills.Any(a => a.Skill.Moniker.Equals(m.Model.SkillMoniker) && a.ProfessionalId == m.ProfessionalId))
            .WithMessage(cmd => $"Professional skills not found with moniker: {cmd.Model.SkillMoniker} and professional: {cmd.ProfessionalId}");

        When(w => !string.IsNullOrEmpty(w.Model.SkillMoniker), () =>
        {
            RuleFor(r => r.Model).Must(m => context.Skills.Any(a => a.Moniker == m.SkillMoniker && a.Name == m.Name))
                .WithMessage(cmd => $"The skill with moniker: {cmd.Model.SkillMoniker} not have a name {cmd.Model.Name}");
        });
    }
}