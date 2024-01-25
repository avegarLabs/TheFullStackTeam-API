using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries.ProfessionalProject;

/// <summary>
/// TODO" Write a description for class GetProjectQueryHandler here.
/// </summary>
public class ReadProfessionalProjectByMonikerQuery : IRequest<ReadProjectByMonikerQueryResult>
{
    public ReadProfessionalProjectByMonikerQuery(string moniker)
    {
        Moniker = moniker;
    }

    public string Moniker { get; set; }
}

/// <summary>
/// TODO: Write a description for class ReadProjectByMonikerQueryResult here.
/// </summary>
public class ReadProjectByMonikerQueryHandler : AppRequestHandler, IRequestHandler<ReadProfessionalProjectByMonikerQuery, ReadProjectByMonikerQueryResult>
{
    public ReadProjectByMonikerQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<ReadProjectByMonikerQueryResult> Handle(ReadProfessionalProjectByMonikerQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Projects.AsNoTracking()
            .Select(ProjectListItem.Projection)
            .Where(p => p.Name == request.Moniker)
            .SingleOrDefaultAsync(cancellationToken);

        if (response == null)
        {
            throw new NotFoundException(nameof(Professional), request.Moniker);
        }

        return new ReadProjectByMonikerQueryResult(response);
    }
}

/// <summary>
/// TODO: Write a description for this class.
/// </summary>
public class ReadProjectByMonikerQueryResult : AppResult<ProjectListItem>
{
    public ReadProjectByMonikerQueryResult(ProjectListItem model) : base(model)
    {
    }
}

/// <summary>
/// TODO: Add a description of the class here
/// </summary>
public sealed class ReadProjectByMonikerQueryValidator : AbstractValidator<ReadProfessionalProjectByMonikerQuery>
{
    public ReadProjectByMonikerQueryValidator(ISessionService sessionService, TheFullStackTeamDbContext context)
    {
        RuleFor(r => r.Moniker).Must(m => context.Projects.Any(p => p.Moniker == m)).WithMessage("Project not found");

        RuleFor(r => r.Moniker).Must(m => context.Projects
                .Any(project => project.Moniker == m
                                && (project.Professional.User.AccountId == sessionService.AccountId()
                                    || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");
    }
}