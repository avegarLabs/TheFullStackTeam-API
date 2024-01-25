using MediatR;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;
using static TheFullStackTeam.Application.General.Queries.ListBasicEntityStatQuery;

namespace TheFullStackTeam.Application.General.Queries;


/// <summary>
/// List of entities to quantity query 
/// </summary>

/// <inheritdoc cref="IRequest{TResponse}"/>
public class ListBasicEntityStatQuery : IRequest<ListBasicEntityStatQueryResult>
{
    public ListBasicEntityStatQuery()
    {
    }


    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
    public class ListQuantityPerEntitiesQueryHandler : IRequestHandler<ListBasicEntityStatQuery, ListBasicEntityStatQueryResult>
    {
        private readonly TheFullStackTeamDbContext _context;

        public ListQuantityPerEntitiesQueryHandler(TheFullStackTeamDbContext context)
        {
            _context = context;
        }

        public async Task<ListBasicEntityStatQueryResult> Handle(ListBasicEntityStatQuery request, CancellationToken cancellationToken)
        {
            var entityList = new List<BasicEntityStatListItem>();

            var userDahboard = new BasicEntityStatListItem()
            {
                EntityName = "Users",
                Quantity = _context.Users.ToList().Count(),
                IconName = "person",

            };

            var professionals = new BasicEntityStatListItem()
            {
                EntityName = "Professionals",
                Quantity = _context.Professionals.ToList().Count(),
                IconName = "account_circle",

            };

            var organizations = new BasicEntityStatListItem()
            {
                EntityName = "Organizations",
                Quantity = _context.Organizations.ToList().Count(),
                IconName = "apartment",
            };

            var institutions = new BasicEntityStatListItem()
            {
                EntityName = "Institutions",
                Quantity = _context.institutions.ToList().Count(),
                IconName = "apartment",

            };

            var skills = new BasicEntityStatListItem()
            {
                EntityName = "Skills",
                Quantity = _context.Skills.ToList().Count(),
                IconName = "directions_run",

            };

            var jobs = new BasicEntityStatListItem()
            {
                EntityName = "Jobs",
                Quantity = _context.Jobs.ToList().Count(),
                IconName = "directions_run",

            };

            entityList.Add(userDahboard);
            entityList.Add(professionals!);
            entityList.Add(organizations!);
            entityList.Add(institutions!);
            entityList.Add(skills!);
            entityList.Add(jobs!);

            return new ListBasicEntityStatQueryResult(entityList);
        }
    }

    /// <inheritdoc cref="AppResult{TModel}"/>
    public class ListBasicEntityStatQueryResult : AppResult<IEnumerable<BasicEntityStatListItem>>
    {
        public ListBasicEntityStatQueryResult(IEnumerable<BasicEntityStatListItem> model) : base(model)
        {
        }
    }
}
