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

/// <summary>
/// Request to create a new professional
/// </summary>
public class CreateProfessionalClientCommand : IRequest<CreateProfessionalClientCommandResult>
{

    public Guid ProfessionalId { get; set; }
    public ClientModel Model { get; set; }
    public CreateProfessionalClientCommand(ClientModel model, Guid id)
    {
        ProfessionalId = id;
        Model = model;
    }

}

/// <summary>
/// Handles the request for creating a new professional client
/// </summary>
public class CreateProfessionalClientCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalClientCommand, CreateProfessionalClientCommandResult>
{
    private readonly IMonikerService _monikerService;

    public CreateProfessionalClientCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService) : base(context)
    {
        _monikerService = monikerService;
    }


    public async Task<CreateProfessionalClientCommandResult> Handle(CreateProfessionalClientCommand request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals
            .Where(p => p.Id == request.ProfessionalId)
            .SingleOrDefaultAsync(cancellationToken);

        if (professional == null)
        {
            throw new NotFoundException(nameof(Professional), request.ProfessionalId);
        }

        Client entity = request.Model;
        entity.Moniker = await _monikerService.FindValidMoniker<Client>(entity.Name);
        entity.ProfessionalId = professional.Id;
        entity.Type = "pro";
        entity.LegalIdentifier = request.Model.LegalIdentifier;

        await _context.Clients.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProfessionalClientCommandResult(entity!);
    }
}

/// <summary>
/// Result of the CreateProfessionalClientCommand.
/// </summary>
public class CreateProfessionalClientCommandResult : AppResult<ClientListItem>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="model">ClientListItem model</param>
    public CreateProfessionalClientCommandResult(ClientListItem model) : base(model)
    {
    }
}

/// <summary>
/// FluentValidation class for the CreateProfessionalClientCommand
/// </summary>
public sealed class CreateProfessionalClientCommandValidator : AbstractValidator<CreateProfessionalClientCommand>
{
    public CreateProfessionalClientCommandValidator(ISessionService sessionService, TheFullStackTeamDbContext context)
    {
        RuleFor(r => r.ProfessionalId).Must(m => context.Professionals
                .Any(professional => professional.Id == m
                                     && (professional.User.AccountId == sessionService.AccountId()
                                         || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");

        RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(Client.NameMaxLength);
        RuleFor(x => x.Model.Email).NotEmpty().EmailAddress().MaximumLength(Client.EmailMaxLength);
        RuleFor(x => x.Model.Phone).NotEmpty().MaximumLength(Client.PhoneMaxLength);
    }
}