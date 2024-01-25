using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.GET;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

public class ReadProfessionalToCVQuery : IRequest<ReadProfessionalToCVQueryResult>
{
    public ReadProfessionalToCVQuery(string moniker)
    {
        Moniker = moniker;
    }

    public string Moniker { get; }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class ReadProfessionalToCVQueryHandler : AppRequestHandler, IRequestHandler<ReadProfessionalToCVQuery, ReadProfessionalToCVQueryResult>
{
    public ReadProfessionalToCVQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<ReadProfessionalToCVQueryResult> Handle(ReadProfessionalToCVQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Professionals.AsNoTracking().Select(ProfessionalCVGet.Projection)
            .Where(f => f.Moniker == request.Moniker).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Professional), request.Moniker);
        }

        return new ReadProfessionalToCVQueryResult(entity);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class ReadProfessionalToCVQueryResult : AppResult<ProfessionalCVGet>
{
    public ReadProfessionalToCVQueryResult(ProfessionalCVGet professionalCVGet) : base(professionalCVGet)
    {
    }
}

public sealed class ReadProfessionalToCVQueryValidator : AbstractValidator<ReadProfessionalToCVQuery>
{
    public ReadProfessionalToCVQueryValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(cmd => cmd.Moniker).Must(moniker => context.Professionals.Any(p => p.Moniker == moniker))
            .WithMessage("Professional does not exist");

        RuleFor(cmd => cmd.Moniker).Must(professionalMoniker => context.Professionals
                .Any(professional => professional.Moniker == professionalMoniker &&
                                     (professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage("You not have a permission to make this operation");
    }
}