using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateProfessionalSkillsCommand : IRequest<CreateProfessionalSkillsCommandResult>
{
    public Guid ProfessionalId { get; }
    public ProfessionalSkillModel Model { get; set; }

    public CreateProfessionalSkillsCommand(ProfessionalSkillModel model, Guid id)
    {
        ProfessionalId = id;
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CreateProfessionalSkillsCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalSkillsCommand, CreateProfessionalSkillsCommandResult>
{
    public readonly IMonikerService _monikerService;

    public CreateProfessionalSkillsCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService) : base(context)
    {
        _monikerService = monikerService;

    }

    public async Task<CreateProfessionalSkillsCommandResult> Handle(CreateProfessionalSkillsCommand request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);

        if (professional == null)
        {
            throw new NotFoundException(nameof(Professional), request.ProfessionalId);
        }
        var skill = await _context.Skills.Where(s => s.Moniker == request.Model.SkillMoniker).SingleOrDefaultAsync(cancellationToken);

        ProfessionalSkill professionalSkill = request.Model;
        professionalSkill.ProfessionalId = professional.Id;

        if (skill == null)
        {
            skill = new Skill()
            {
                Moniker = await _monikerService.FindValidMoniker<Skill>(request.Model.Name),
                Name = request.Model.Name,
                ProfessionalSkills = new List<ProfessionalSkill> { professionalSkill }
            };
            await _context.Skills.AddAsync(skill, cancellationToken);
        }
        else
        {
            skill.ProfessionalSkills.Add(professionalSkill);
            _context.Skills.Update(skill);
        }

        await _context.SaveChangesAsync(cancellationToken);

        ProfessionalSkillListItem result = professionalSkill;
        result.SkillMoniker = skill?.Moniker;

        return new CreateProfessionalSkillsCommandResult(result);
    }


}

/// <inheritdoc cref="AppResult{TModel}"/>
public class CreateProfessionalSkillsCommandResult : AppResult<ProfessionalSkillListItem>
{
    public CreateProfessionalSkillsCommandResult(ProfessionalSkillListItem model) : base(model)
    {
    }
}

public sealed class CreateProfessionalSkillsCommandValidator : AbstractValidator<CreateProfessionalSkillsCommand>
{
    public CreateProfessionalSkillsCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(r => r.ProfessionalId).Must(m => context.Professionals
                .Any(professional => professional.Id == m &&
                                     (professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");

        /**When(w => !string.IsNullOrEmpty(w.Model.SkillMoniker), () =>
        {
            RuleFor(r => r.Model).Must(m => context.Skills.Any(a => a.Moniker == m.SkillMoniker && a.Name == m.Name))
                .WithMessage(cmd => $"The skill with moniker: {cmd.Model.SkillMoniker} not have a name {cmd.Model.Name}");
        });*/
    }
}