using FluentValidation;
using MediatR;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;
using WindowsAzure.Table.Extensions;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <summary>
/// Request to Delete a new professional client
/// </summary>
public class DeleteProfessionalClientCommand : IRequest<DeleteProfessionalClientCommandResult>
{

    public Guid ClientId { get; set; }

    public DeleteProfessionalClientCommand(Guid id)
    {
        ClientId = id;
    }

}

/// <summary>
/// Handler for the DeleteProfessionalClientCommand
/// </summary>
public class DeleteProfessionalClientCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalClientCommand, DeleteProfessionalClientCommandResult>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Data context</param>
    public DeleteProfessionalClientCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<DeleteProfessionalClientCommandResult> Handle(DeleteProfessionalClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .Where(p => p.Id == request.ClientId)
            .SingleOrDefaultAsync(cancellationToken);

        if (client == null)
        {
            throw new NotFoundException(nameof(Client), request.ClientId);
        }
        Console.Write(request.ClientId);

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteProfessionalClientCommandResult(true);
    }
}

/// <summary>
/// Result of the command.
/// </summary>
public class DeleteProfessionalClientCommandResult : AppResult<bool>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="model">Boolean model</param>
    public DeleteProfessionalClientCommandResult(bool model) : base(model)
    {
    }
}

/// <summary>
/// DeleteProfessionalClientCommandValidator class.
/// </summary>
public sealed class DeleteProfessionalClientCommandValidator : AbstractValidator<DeleteProfessionalClientCommand>
{
    public DeleteProfessionalClientCommandValidator(ISessionService sessionService, TheFullStackTeamDbContext context)
    {
        RuleFor(r => r.ClientId).NotEmpty()
            .Must(id => context.Clients.Any(a => a.Id == id))
            .WithMessage(_ => "Client not found");
    }
}