using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application;

public abstract class AppRequestHandler
{
    protected readonly TheFullStackTeamDbContext _context;

    protected AppRequestHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }
}

