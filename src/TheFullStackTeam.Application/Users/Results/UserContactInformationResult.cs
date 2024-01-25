using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.Application.Users.Results
{
    public class UserContactInformationResult : AppResult<ContactInformationModel>
    {
        public UserContactInformationResult(ContactInformationModel model) : base(model) { }
    }
}
