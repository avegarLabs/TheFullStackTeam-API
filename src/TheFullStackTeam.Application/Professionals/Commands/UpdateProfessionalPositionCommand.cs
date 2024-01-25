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
public class UpdateProfessionalPositionCommand : IRequest<UpdateProfessionalExperienceCommandResult>
{
    public Guid ProfessionalId { get; }
    public Guid PositionId { get; }
    public PositionModel Model { get; set; }

    public UpdateProfessionalPositionCommand(PositionModel model, Guid id, Guid positionId)
    {
        ProfessionalId = id;
        PositionId = positionId;
        Model = model;

    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class UpdateProfessionalExperienceCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalPositionCommand, UpdateProfessionalExperienceCommandResult>
{
    private readonly IMonikerService _monikerService;
    private readonly IConfiguration _configuration;

    public UpdateProfessionalExperienceCommandHandler(TheFullStackTeamDbContext context, IConfiguration configuration, IMonikerService moniker) : base(context)
    {
        _configuration = configuration;
        _monikerService = moniker;
    }

    public async Task<UpdateProfessionalExperienceCommandResult> Handle(UpdateProfessionalPositionCommand request, CancellationToken cancellationToken)
    {
        var experience = await _context.Positions
             .Where(p => p.Id == request.PositionId && p.ProfessionalId == request.ProfessionalId)
             .SingleOrDefaultAsync(cancellationToken);

        if (experience == null)
        {
            throw new NotFoundException(nameof(Position), request.Model.Name);
        }
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

         try
         {
            if (request.Model.Type.Equals("org"))
            {
                var organization = await _context.Organizations.Where(o => o.Moniker.Equals(request.Model.OrganizationMoniker)).SingleOrDefaultAsync(cancellationToken);
                organization.Positions.Add(experience);
                _context.Organizations.Update(organization);
                experience.OrganizationId = organization.Id;

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
                        Positions = new List<Position> { experience }
                    };
                    await _context.institutions.AddAsync(institution);
                    experience.InstitutionId = institution.Id;
                }
                else
                {
                    institution.Positions.Add(experience);
                    _context.institutions.Update(institution);
                    experience.InstitutionId = institution.Id;

                }
            }

            experience.Description = request.Model.Description;
            experience.Name = request.Model.Name;
            experience.StartMonthYear = request.Model.StartMonthYear;
            experience.EndMonthYear = request.Model.EndMonthYear;
          
            _context.Positions.Update(experience);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new UpdateProfessionalExperienceCommandResult(experience);

        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}

/// <summary>
/// Result class
/// </summary>
public class UpdateProfessionalExperienceCommandResult : AppResult<PositionListItem>
{
    public UpdateProfessionalExperienceCommandResult(PositionListItem model) : base(model)
    {
    }
}

public sealed class UpdateProfessionalExperienceCommandValidator : AbstractValidator<UpdateProfessionalPositionCommand>
{
    public UpdateProfessionalExperienceCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(r => r.ProfessionalId).Must(m => context.Professionals
                   .Any(professional => professional.Id == m &&
                                        (professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
               .WithMessage(_ => "You not have a permission to make this operation");

        When(cmd => cmd.Model.EndMonthYear.HasValue, () =>
        {
            RuleFor(cmd => cmd.Model).Must(date => date.EndMonthYear > date.StartMonthYear)
            .WithMessage("Date end must be greater than date start");
        });

        RuleFor(cmd => cmd.Model).Must(date => date.EndMonthYear > date.StartMonthYear)
            .WithMessage("Date end must be greater than date start");

    }
}