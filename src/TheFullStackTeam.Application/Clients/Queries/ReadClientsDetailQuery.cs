using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Clients.Queries;

/// <summary>
/// TODO: Add a description of the class here
/// </summary>
public class ReadClientsDetailQuery : IRequest<ReadClientsDetailQueryResult>
{
    public ReadClientsDetailQuery(string moniker)
    {
        Moniker = moniker;
    }

    public string Moniker { get; set; }
}

/// <summary>
/// TODO: Add a description of the class here
/// </summary>
public class ReadClientsDetailQueryHandler : AppRequestHandler, IRequestHandler<ReadClientsDetailQuery, ReadClientsDetailQueryResult>
{
    public ReadClientsDetailQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<ReadClientsDetailQueryResult> Handle(ReadClientsDetailQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Clients.AsNoTracking()
            .Where(p => p.Moniker == request.Moniker)
            .Select(ClientListItem.Projection)
            .SingleOrDefaultAsync(cancellationToken);

        if (response == null)
        {
            throw new NotFoundException(nameof(Client), request.Moniker);
        }

        return new ReadClientsDetailQueryResult(response);
    }
}

/// <summary>
/// TODO: Add a description of the class here
/// </summary>
public class ReadClientsDetailQueryResult : AppResult<ClientListItem>
{
    public ReadClientsDetailQueryResult(ClientListItem data) : base(data)
    {
    }
}

/// <summary>
/// TODO: Add a description of the class here
/// </summary>
public sealed class ReadClientsDetailQueryValidator : AbstractValidator<ReadClientsDetailQuery>
{
    public ReadClientsDetailQueryValidator(ISessionService sessionService, TheFullStackTeamDbContext context)
    {
        RuleFor(r => r.Moniker).Must(m => context.Clients.Any(a => a.Moniker == m)).WithMessage("Client not found");

        RuleFor(r => r.Moniker).Must(m => context.Clients
                .Any(client => client.Moniker == m
                               && (client.Professional.User.AccountId == sessionService.AccountId()
                                   || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");
    }
}