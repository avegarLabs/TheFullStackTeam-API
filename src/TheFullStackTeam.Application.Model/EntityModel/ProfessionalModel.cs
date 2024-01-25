using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class ProfessionalModel
    {
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string AboutMe { get; set; } = null!;
        public string Industry { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;

        public string PersonalWeb { get; set; } = null!;

        public string LinkedInProfile { get; set; } = null!;

        public string YoutubeProfile { get; set; } = null!;


        public static implicit operator Professional(ProfessionalModel model) => new()
        {
            Name = model.Name,
            AboutMe = model.AboutMe,
            Title = model.Title,
            Industry = model.Industry,
            Phone = model.Phone,
            ContactEmail = model.ContactEmail,
            PersonalWeb = model.PersonalWeb,
            LinkedInProfile = model.LinkedInProfile,
            YoutubeProfile = model.YoutubeProfile
        };
    }
}
