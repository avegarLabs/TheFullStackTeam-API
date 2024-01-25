using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Communications.EmailTemplates.ViewModels;
using TheFullStackTeam.Communications.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateProfessionalCommand : IRequest<CreateProfessionalCommandResult>
{
    public ProfessionalModel Professional { get; set; }

    public CreateProfessionalCommand(ProfessionalModel professional)
    {
        Professional = professional;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CreateProfessionalCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalCommand, CreateProfessionalCommandResult>
{
    private readonly IMonikerService _monikerService;
    private readonly ISessionService _sessionService;
    private readonly IEmailService _emailService;

    public CreateProfessionalCommandHandler(TheFullStackTeamDbContext context,
        IMonikerService monikerService, ISessionService sessionService, IEmailService emailService) : base(context)
    {
        _monikerService = monikerService;
        _sessionService = sessionService;
        _emailService = emailService;

    }

    public async Task<CreateProfessionalCommandResult> Handle(CreateProfessionalCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(p => p.AccountId == _sessionService.AccountId())
            .FirstOrDefaultAsync(cancellationToken);

        Professional entity = request.Professional;
        entity.Name = user.Name;
        entity.UserId = user.Id;
        entity.Moniker = await _monikerService.FindValidMoniker<Professional>(user.Name);
        entity.CountryId = user.Country.Id;
        await _context.Professionals.AddAsync(entity, cancellationToken);
        await sentEmailToProfessional(user, entity);
        await _context.SaveChangesAsync(cancellationToken);



        return new CreateProfessionalCommandResult(entity!);
    }

    private Task sentEmailToProfessional(User user, Professional entity)
    {
        Console.WriteLine("Contactando a:" + user.FirstName);
        var dataModel = new ProfessionalCreatedNotificationViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = entity.Name,
            Email = user.ContactEmail,
            Moniker = entity.Moniker,
            Moto = entity.Title
        };
        _emailService.SendProfessionalWelcomeEmail(dataModel, user.ContactEmail);

        Console.WriteLine("Contactando a:" + user.FirstName);

        return Task.CompletedTask;
    }
}



/// <inheritdoc cref="AppResult{TModel}"/>
public class CreateProfessionalCommandResult : AppResult<ProfessionalListItem>
{
    public CreateProfessionalCommandResult(ProfessionalListItem model) : base(model)
    {
    }
}

/// <summary>
/// Create a professional command validator
/// </summary>
public sealed class CreateProfessionalCommandValidator : AbstractValidator<CreateProfessionalCommand>
{
    public CreateProfessionalCommandValidator(ISessionService sessionService, TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Professional).Must(_ => context.Users.Any(a => a.AccountId == sessionService.AccountId()))
            .WithMessage("You must be logged in to create a professional");
        RuleFor(x => x.Professional.AboutMe).NotEmpty().MaximumLength(Professional.AboutMeMaxLenght);
        RuleFor(x => x.Professional.Title).NotEmpty().MaximumLength(Professional.AboutMeMaxLenght);
    }
}