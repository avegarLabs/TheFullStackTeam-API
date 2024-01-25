using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Queries;

/// <summary>
/// A professional honors list query 
/// </summary>
public class ListProfessionalHonorsQuery : IRequest<ListProfessionalHonorsQueryResult>
{
    public Guid ProfessionalId { get; }

    public ListProfessionalHonorsQuery(Guid id)
    {
        ProfessionalId = id;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class ListProfessionalHonorsQueryHandler : IRequestHandler<ListProfessionalHonorsQuery, ListProfessionalHonorsQueryResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public ListProfessionalHonorsQueryHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<ListProfessionalHonorsQueryResult> Handle(ListProfessionalHonorsQuery request, CancellationToken cancellationToken)
    {
        var professionalHonors = await _context.Honors
            .Where(ps => ps.ProfessionalId == request.ProfessionalId)
            .Select(HonorListItem.Projection)
            .ToListAsync(cancellationToken);

        return new ListProfessionalHonorsQueryResult(professionalHonors);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class ListProfessionalHonorsQueryResult : AppResult<IEnumerable<HonorListItem>>
{
    public ListProfessionalHonorsQueryResult(IEnumerable<HonorListItem> model) : base(model)
    {
    }
}