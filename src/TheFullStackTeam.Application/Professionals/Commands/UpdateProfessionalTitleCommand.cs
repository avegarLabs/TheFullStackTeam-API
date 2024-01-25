using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

public class UpdateProfessionalTitleCommand : IRequest<UpdateProfessionalTitleCommandResult>
{
    public Guid ProfessionalId { get; }
    public Guid TitleId { get; }
    public TitleModel Model { get; set; }

    public UpdateProfessionalTitleCommand(TitleModel model, Guid id, Guid tId)
    {
        ProfessionalId = id;
        TitleId = tId;
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class UpdateProfessionalTitleCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalTitleCommand, UpdateProfessionalTitleCommandResult>
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<UpdateProfessionalTitleCommandHandler> _logger;
    private readonly IMonikerService _monikerService;

    public UpdateProfessionalTitleCommandHandler(TheFullStackTeamDbContext context, IConfiguration configuration, ILogger<UpdateProfessionalTitleCommandHandler> logger, IMonikerService moniker) : base(context)
    {
        _configuration = configuration;
        _logger = logger;
        _monikerService = moniker;

    }

    public async Task<UpdateProfessionalTitleCommandResult> Handle(UpdateProfessionalTitleCommand request, CancellationToken cancellationToken)
    {

        var title = await _context.Titles
            .Where(t => t.Id == request.TitleId && t.ProfessionalId == request.ProfessionalId)
            .SingleOrDefaultAsync(cancellationToken);

        if (title == null)
        {
            throw new NotFoundException(nameof(Title), request.TitleId);
        }

       var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            if (request.Model.Type.Equals("org"))
            {
                var organization = await _context.Organizations.Where(o => o.Moniker.Equals(request.Model.OrganizationMoniker)).SingleOrDefaultAsync(cancellationToken);
                title.OrganizationId = organization?.Id;
                organization.Titles.Add(title);
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
                        Titles = new List<Title> { title }
                    };
                    await _context.institutions.AddAsync(institution);
                }
                else
                {
                    institution.Titles.Add(title);
                    title.InstitutionId = institution?.Id;
                    _context.institutions.Update(institution);
                }
            }

            title.StartMonthYear = request.Model.StartMonthYear;
            title.EndMonthYear = request.Model.EndMonthYear;
            title.Name = request.Model.Name;
            title.OrganizationName = request.Model.OrganizationName;
           

            _context.Titles.Update(title);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new UpdateProfessionalTitleCommandResult(title);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on update title");
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

}



/// <summary>
/// Result class
/// </summary>
public class UpdateProfessionalTitleCommandResult : AppResult<TitleListItem>
{
    public UpdateProfessionalTitleCommandResult(TitleListItem model) : base(model)
    {
    }
}

public sealed class UpdateProfessionalTitleCommandValidator : AbstractValidator<UpdateProfessionalTitleCommand>
{
    public UpdateProfessionalTitleCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {

        RuleFor(r => r.ProfessionalId).Must(m => context.Professionals
                .Any(professional => professional.Id == m &&
                                     (professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");

        RuleFor(cmd => cmd.Model.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(cmd => cmd.Model.OrganizationName).NotEmpty().WithMessage("Organization name is required");
        RuleFor(cmd => cmd.Model.StartMonthYear).NotEmpty().Must(date => date.Date < DateTime.UtcNow.Date)
            .WithMessage("Date must be greater than today");

        When(cmd => cmd.Model.EndMonthYear.HasValue, () =>
        {
            RuleFor(cmd => cmd.Model).Must(date => date.EndMonthYear > date.StartMonthYear)
            .WithMessage("Date end must be greater than date start");
        });
    }
}

