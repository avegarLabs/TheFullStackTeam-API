using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Results
{
    public class ProfessionalLanguageCommanResults: AppResult<ProfessionalLanguegeListItem>
    {

        public ProfessionalLanguageCommanResults(ProfessionalLanguegeListItem model):base(model) { }
    }
}
