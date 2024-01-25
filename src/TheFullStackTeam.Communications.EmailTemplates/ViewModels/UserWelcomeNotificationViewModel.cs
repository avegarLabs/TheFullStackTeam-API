namespace TheFullStackTeam.Communications.EmailTemplates.ViewModels
{
    public class UserWelcomeNotificationViewModel
    {
        public string Email { get; set; }
        public string AccountId { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string  MarketPlaceBaseUrl { get; set; } 
    }
}
