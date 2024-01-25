using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class ProfessionalLanguegeListItem
    {
        public Guid Id { get; set; }
        public Guid? LanguageId { get; set; }
        public string Name { get; set; }    
        public string Level { get; set; }
      
        public static implicit operator ProfessionalLanguegeListItem(ProfessionalLanguage domainEntity) => new()
        {
            Id = domainEntity.Id,
            LanguageId = domainEntity.LanguegeId,
            Name = domainEntity.LanguegeName,
            Level = domainEntity.Level

        };

        public static Expression<Func<ProfessionalLanguage, ProfessionalLanguegeListItem>> Projection =>
            x => new ProfessionalLanguegeListItem
            {
                Id = x.Id,
                LanguageId = x.LanguegeId,
                Name = x.LanguegeName,
                Level = x.Level
            };
    }
}
