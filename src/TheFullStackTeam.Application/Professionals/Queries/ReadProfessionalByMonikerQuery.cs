using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

public class ReadProfessionalByMonikerQuery : IRequest<ProfessionalQueryResult>
{
    public ReadProfessionalByMonikerQuery(string moniker)
    {
        Moniker = moniker;
    }

    public string Moniker { get; }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class ReadProfessionalByMonikerQueryHandler : AppRequestHandler, IRequestHandler<ReadProfessionalByMonikerQuery, ProfessionalQueryResult>
{

     public ReadProfessionalByMonikerQueryHandler(TheFullStackTeamDbContext context) : base(context)
    {
       
    }

    public async Task<ProfessionalQueryResult> Handle(ReadProfessionalByMonikerQuery request, CancellationToken cancellationToken)
    {
              
        var entity = await _context.Professionals.AsNoTracking().Select(ProfessionalListItem.Projection)
            .FirstOrDefaultAsync(f => f.Moniker.Equals(request.Moniker), cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Professional), request.Moniker);
        }
       

        return new ProfessionalQueryResult(entity);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class ProfessionalQueryResult : AppResult<ProfessionalListItem>
{
    public ProfessionalQueryResult(ProfessionalListItem professionalItem) : base(professionalItem)
    {
    }
    public ProfessionalQueryResult()
    {
    }
}



