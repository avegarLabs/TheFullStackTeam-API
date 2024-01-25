using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.CvPdfGenerator.ViewModels
{
    public class ProfessionalVitaeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string AboutMe { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string PersonalWeb { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string Image { get; set; }
        public List<ProfessionalSkillListItem> skillListItems { get; set; } = null!;
        public List<TitleListItem> titleListItems { get; set; } = null!;
        public List<PositionListItem> positionListItems { get; set; } = null!;
        public List<ProfessionalLanguegeListItem> professionalLanguegeListItems { get; set; } = null!;
    }
}
