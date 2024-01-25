using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateProfessionalPositionCommand : IRequest<CreateProfessionalExperienceCommandResult>
{
    public Guid ProfessionalId { get; }
    public PositionModel Model { get; set; }
    public CreateProfessionalPositionCommand(PositionModel model, Guid id)
    {
        ProfessionalId = id;
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CreateProfessionalExperienceCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalPositionCommand, CreateProfessionalExperienceCommandResult>
{
    private readonly IMonikerService _monikerService;
    private readonly IConfiguration _configuration;

    public CreateProfessionalExperienceCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService, IConfiguration configuration) : base(context)
    {
        _monikerService = monikerService;
        _configuration = configuration;
    }

    public async Task<CreateProfessionalExperienceCommandResult> Handle(CreateProfessionalPositionCommand request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
        if (professional == null)
        {
            throw new NotFoundException(nameof(Professional), request.ProfessionalId);
        }

        Position entity = request.Model;
        entity.ProfessionalId = professional.Id;

        if (request.Model.Type.Equals("org"))
        {
            var organization = await _context.Organizations.Where(o => o.Moniker.Equals(request.Model.OrganizationMoniker)).SingleOrDefaultAsync(cancellationToken);
            organization.Positions.Add(entity);
            _context.Organizations.Update(organization);

        }
        else if (request.Model.Type.Equals("inst"))
        {
            var imageUrl = _configuration["OraganizationNoImage"];
            var institution = await _context.institutions.Where(o => o.Moniker.Equals(request.Model.OrganizationMoniker)).SingleOrDefaultAsync(cancellationToken);
            if (institution == null)
            {
                institution = new Domain.Entities.Institution()
                {
                    Name = request.Model.OrganizationName,
                    Moniker = await _monikerService.FindValidMoniker<Professional>(request.Model.OrganizationName),
                    Description = " ",
                    Logo = new ImageUrl { ThumbUrl = imageUrl, DisplayUrl = imageUrl },
                    Positions = new List<Position> { entity }
                };
                await _context.institutions.AddAsync(institution);
            }
            else
            {
                institution.Positions.Add(entity);
                _context.institutions.Update(institution);
            }
        }
       
        if (request.Model.SkillList.Count()>0)
        {
           entity.SkillPositions =  await AddSkillToPosition(request.Model.SkillList, cancellationToken);
        }
        await _context.Positions.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProfessionalExperienceCommandResult(entity);
    }

    private async Task<List<Skill>> AddSkillToPosition(IEnumerable<string> skillVersionsModels, CancellationToken cancellationToken)
    {
        List<Skill> tempList = new List<Skill>();

        foreach (var item in skillVersionsModels)
        {
            var skill = await _context.Skills
               .Where(s => s.Moniker.ToLower().Equals(item.ToLower().Trim()))
               .SingleOrDefaultAsync(cancellationToken);

            if (skill == null)
            {
                skill = new Skill()
                {
                    Moniker = await _monikerService.FindValidMoniker<Skill>(item.Trim()),
                    Name = item
                };
                await _context.Skills.AddAsync(skill, cancellationToken);
            }
            tempList.Add(skill);
        }
        return tempList;
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class CreateProfessionalExperienceCommandResult : AppResult<PositionListItem>
{
    public CreateProfessionalExperienceCommandResult(PositionListItem model) : base(model)
    {
    }
}

public sealed class CreateProfessionalExperienceCommandValidator : AbstractValidator<CreateProfessionalPositionCommand>
{
    public CreateProfessionalExperienceCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(r => r.ProfessionalId).Must(m => context.Professionals
                .Any(professional => professional.Id == m &&
                                     (professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");

        When(cmd => cmd.Model.EndMonthYear.HasValue, () =>
        {
            RuleFor(cmd => cmd.Model).Must(date => date.StartMonthYear < date.EndMonthYear)
            .WithMessage("Date end must be greater than date start");
        });
    }
}

