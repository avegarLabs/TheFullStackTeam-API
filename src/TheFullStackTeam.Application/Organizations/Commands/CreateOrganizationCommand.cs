using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Communications.EmailTemplates.ViewModels;
using TheFullStackTeam.Communications.Services.Abstract;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateOrganizationCommand : IRequest<CreateOrganizationCommandResult>
{
    public OrganizationModel Model { get; set; }

    public CreateOrganizationCommand(OrganizationModel model)
    {
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CreateOrganizationCommandHandler : AppRequestHandler, IRequestHandler<CreateOrganizationCommand, CreateOrganizationCommandResult>
{
    private readonly IMonikerService _monikerService;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    public CreateOrganizationCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker, IConfiguration configuration, IEmailService email) : base(context)
    {
        _monikerService = moniker;
        _configuration = configuration;
        _emailService = email;
    }

    public async Task<CreateOrganizationCommandResult> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var orgMoniker = await _monikerService.FindValidMoniker<Domain.Entities.Organization>(request.Model.Name);
        var user = await _context.Users.Where(u => u.AccountId.Equals(request.Model.AccountId)).SingleOrDefaultAsync(cancellationToken);
        var imageUrl = _configuration["OraganizationNoImage"];
        var org = new Domain.Entities.Organization()
        {
            Moniker = orgMoniker,
            Name = request.Model.Name,
            Description = request.Model.Description,
            UserId = user.Id,
            CountryId =  request.Model.Country.Id,
            Phone = user.Phone,
            ContactEmail = user.ContactEmail,
            LinkedInProfile = "",
            OrganizationWeb = "",
            YoutubeProfile = "",
            Sector = request.Model.Sector,
            Zise = request.Model.Zise,
            Logo = new ImageUrl { ThumbUrl = imageUrl, DisplayUrl = imageUrl },
        };

        await _context.AddAsync(org, cancellationToken);
        await sentEmailToOrganizations(user,org);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateOrganizationCommandResult(org);
    }

    private Task sentEmailToOrganizations(Domain.Entities.User? user, Domain.Entities.Organization org)
    {
        var dataModel = new ProfessionalCreatedNotificationViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = org.Name,
            Email = org.ContactEmail,
            Moniker = org.Moniker,
            Moto = org.Sector
        };
        _emailService.SendProfessionalWelcomeEmail(dataModel, org.ContactEmail);
        return Task.CompletedTask;
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class CreateOrganizationCommandResult : AppResult<OrganizationListItem>
{
    public CreateOrganizationCommandResult(OrganizationListItem response) : base(response)
    {
    }
}

// <summary>
/// Fluent create organization command validator
/// </summary>
public sealed class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(Domain.Entities.Organization.NameMaxLenght);
    }
}