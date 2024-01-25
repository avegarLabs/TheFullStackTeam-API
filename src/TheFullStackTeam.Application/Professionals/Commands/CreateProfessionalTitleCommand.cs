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
public class CreateProfessionalTitleCommand : IRequest<CreateProfessionalTitleCommandResult>
{
    public Guid ProfessionalId { get; }
    public TitleModel Model { get; set; }
    public CreateProfessionalTitleCommand(TitleModel model, Guid id)
    {
        ProfessionalId = id;
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CreateProfessionalTitleCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalTitleCommand, CreateProfessionalTitleCommandResult>
{
    private readonly IConfiguration _configuration;
    private readonly IMonikerService _monikerService;

    public CreateProfessionalTitleCommandHandler(TheFullStackTeamDbContext context, IConfiguration configuration, IMonikerService moniker) : base(context)
    {
        _configuration = configuration;
        _monikerService = moniker;
    }

    public async Task<CreateProfessionalTitleCommandResult> Handle(CreateProfessionalTitleCommand request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
        if (professional == null)
        {
            throw new NotFoundException(nameof(Professional), request.ProfessionalId);
        }

        Title entity = request.Model;
        entity.ProfessionalId = professional.Id;

        if (request.Model.Type.Equals("org"))
        {
            var organization = await _context.Organizations.Where(o => o.Moniker.Equals(request.Model.OrganizationMoniker)).SingleOrDefaultAsync(cancellationToken);
            entity.OrganizationId = organization?.Id;
            organization.Titles.Add(entity);
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
                    Titles = new List<Title> { entity }
                };
                await _context.institutions.AddAsync(institution);
            }
            else
            {
                institution.Titles.Add(entity);
                entity.InstitutionId = institution?.Id;
                _context.institutions.Update(institution);
            }
        }
       await _context.SaveChangesAsync(cancellationToken);
       return new CreateProfessionalTitleCommandResult(entity);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class CreateProfessionalTitleCommandResult : AppResult<TitleListItem>
{
    public CreateProfessionalTitleCommandResult(TitleListItem model) : base(model)
    {
    }
}

public sealed class CreateProfessionalTitleCommandValidator : AbstractValidator<CreateProfessionalTitleCommand>
{
    public CreateProfessionalTitleCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(r => r.ProfessionalId).Must(m => context.Professionals
                .Any(professional => professional.Id == m &&
                                     (professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");

        RuleFor(cmd => cmd.Model.StartMonthYear).NotEmpty().Must(date => date.Date < DateTime.UtcNow.Date)
           .WithMessage("Date must be greater than today");

        When(cmd => cmd.Model.EndMonthYear.HasValue, () =>
        {
            RuleFor(cmd => cmd.Model).Must(date => date.EndMonthYear > date.StartMonthYear)
            .WithMessage("Date end must be greater than date start");
        });


    }
}

