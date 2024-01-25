using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities.Base;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Services;

public class MonikerService : IMonikerService
{
    private readonly Func<string, int, string> _newMoniker = (moniker, count) =>
    {
        var newMoniker =
            (count != 0 ? $"{moniker}{count + 1}" : moniker).ToLower();
        return newMoniker.Length > 50 ? newMoniker[..50] : newMoniker;
    };

    private readonly TheFullStackTeamDbContext _context;
    private readonly Regex _rgx;

    private string PrepareMoniker(string suggestedMoniker)
    {
        const RegexOptions options = RegexOptions.None;
        var regex = new Regex("[ ]{2,}", options);
        suggestedMoniker = regex.Replace(suggestedMoniker, " ");
        suggestedMoniker = suggestedMoniker.Trim();
        suggestedMoniker = suggestedMoniker
            .Replace(" ", "-")
            .Replace("_", "-")
            .Replace("á", "a")
            .Replace("é", "e")
            .Replace("í", "i")
            .Replace("ó", "o")
            .Replace("ú", "u")
            .Replace("ü", "u")
            .Replace("ç", "c")
            .Replace("ñ", "n")
            .Replace("Á", "a")
            .Replace("É", "e")
            .Replace("Í", "i")
            .Replace("Ó", "o")
            .Replace("Ú", "u")
            .Replace("ü", "u")
            .Replace("Ç", "c")
            .Replace("Ñ", "n");
        suggestedMoniker = suggestedMoniker.Trim();
        return _rgx.Replace(suggestedMoniker, "").ToLower();
    }

    public MonikerService(TheFullStackTeamDbContext context)
    {
        _context = context;
        _rgx = new Regex("[^a-zA-Z0-9 -]");
    }

    public async Task<string> FindValidMoniker<TEntity>(string suggestedMoniker) where TEntity : NicknamedEntity
    {
        var moniker = PrepareMoniker(suggestedMoniker);
        var count = await _context.Set<TEntity>().CountAsync(o => o.Moniker.StartsWith(moniker));
        return _newMoniker(moniker, count);
    }
}