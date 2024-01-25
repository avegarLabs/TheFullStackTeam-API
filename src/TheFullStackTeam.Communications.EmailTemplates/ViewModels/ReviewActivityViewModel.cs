using System;

namespace TheFullStackTeam.Communications.EmailTemplates.ViewModels
{
    public class ReviewActivityViewModel
    {
        public string PartnerMoniker { get; set; }
        public string PartnerName { get; set; }

        public string UserProfileMoniker { get; set; }
        public string UserProfileName { get; set; }

        public string ReviewUrl { get; set; }
        public string PartnerEmail { get; set; }
        public string ActyivityName { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
