using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class LanguageModel
    {
        public string Name { get; set; } = null!;
        public string LocalName { get; set; } = null!;
        public string IsoCode { get; set; } = null!;
        public string ThreeLetterIsoCode { get; set; } = null!;
        public int LCID { get; set; }

        public static implicit operator Language(LanguageModel model) => new()
        {
            Name = model.Name,
            LocalName = model.LocalName,
            IsoCode = model.IsoCode,
            ThreeLetterIsoCode= model.ThreeLetterIsoCode,
            LCID= model.LCID
         
        };
    }
}
