using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class JobLanguagueListItem
{
    public LanguageListItem? Languegue { get; set; }
 
     public static Expression<Func<JobLanguage, JobLanguagueListItem>> Projection =>
        x => new JobLanguagueListItem
        {
            Languegue =  x.Language
        };
}