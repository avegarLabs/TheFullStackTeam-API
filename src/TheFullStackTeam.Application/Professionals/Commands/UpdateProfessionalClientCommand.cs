using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

public class UpdateProfessionalClientCommand : IRequest<UpdateProfessionalClientResult>
{

    public Guid ProfessionalId { get; set; }
    public Guid ClientId { get; set; }
    public ClientModel Model { get; set; }
    public UpdateProfessionalClientCommand(Guid pId, Guid cId, ClientModel model)
    {
        ProfessionalId = pId;
        ClientId = cId;
        Model = model;
    }

}

public class UpdateProfessionalClientCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalClientCommand, UpdateProfessionalClientResult>
{
    public UpdateProfessionalClientCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<UpdateProfessionalClientResult> Handle(UpdateProfessionalClientCommand request, CancellationToken cancellationToken)
    {
        var entity  = await _context.Clients.AsNoTracking()
            .Where(p => p.Id == request.ClientId && p.ProfessionalId == request.ProfessionalId)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Client), request.ClientId);
        }

         entity.LegalIdentifier = request.Model.LegalIdentifier;
         entity.Name = request.Model.Name;
         entity.Phone= request.Model.Phone;
         entity.Email = request.Model.Email;
         entity.LegalName= request.Model.LegalName;

        _context.Clients.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateProfessionalClientResult(entity);
    }
}

public class UpdateProfessionalClientResult : AppResult<ClientListItem>
{
    public UpdateProfessionalClientResult(ClientListItem model) : base(model)
    {
    }
}

public sealed class UpdateProfessionalClientCommandValidator : AbstractValidator<UpdateProfessionalClientCommand>
{
    public UpdateProfessionalClientCommandValidator(ISessionService sessionService, TheFullStackTeamDbContext context)
    {
        RuleFor(r => r.ProfessionalId).Must(m => context.Clients
                .Any(client => client.ProfessionalId == m &&
                               (client.Professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");

        RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(Client.NameMaxLength);
        RuleFor(x => x.Model.Email).NotEmpty().EmailAddress().MaximumLength(Client.EmailMaxLength);
        RuleFor(x => x.Model.Phone).NotEmpty().MaximumLength(Client.PhoneMaxLength);
    }
}