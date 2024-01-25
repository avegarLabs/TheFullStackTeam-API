using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class ProfessionalLanguegeModel
    {
         public string Level { get; set; }
         public LanguageListItem Language { get; set; }

        public static implicit operator ProfessionalLanguage(ProfessionalLanguegeModel model) => new()
        {
            Level = model.Level
        };
    }
}
