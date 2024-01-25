using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class LanguageListItem
    {
        public Guid Id { get; set; }    
        public string Name { get; set; } = null!;
        public string LocalName { get; set; } = null!;
        public string IsoCode { get; set; } = null!;
        public string ThreeLetterIsoCode { get; set; } = null!;
        public int LCID { get; set; }

        public static implicit operator LanguageListItem(Language domainEntity) => new()
        {
            Id = domainEntity.Id,
           Name = domainEntity.Name,
           LocalName = domainEntity.LocalName,
           IsoCode = domainEntity.IsoCode,
           ThreeLetterIsoCode= domainEntity.ThreeLetterIsoCode,
           LCID = domainEntity.LCID
        };


        public static Expression<Func<Language, LanguageListItem>> Projection =>
       x => new LanguageListItem
       {
           Id = x.Id,
           Name = x.Name,
           LocalName = x.LocalName,
           IsoCode = x.IsoCode,
           ThreeLetterIsoCode= x.ThreeLetterIsoCode,
           LCID = x.LCID
          
       };

    }
}
