using System;

namespace TheFullStackTeam.Communications.EmailTemplates.ViewModels
{
    public class ReviewsNotficationViewModel
    {
        public string PartnerEmail { get; set; }
        public string PartnerDisplayName { get; set; }
        public string ActivityName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorDisplayName { get; set; }
        public DateTime Created { get; set; }
        public string ReviewName { get; set; }
        public int Rating { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }

        public ReviewsNotficationViewModel(string partnerEmail, string partnerDisplayName, string activityName,
            string authorEmail, string authorDisplayName, DateTime created, string reviewName, int rating, string body, string url)
        {
            PartnerEmail = partnerEmail;
            PartnerDisplayName = partnerDisplayName;
            ActivityName = activityName;
            AuthorEmail = authorEmail;
            AuthorDisplayName = authorDisplayName;
            Created = created;
            ReviewName = reviewName;
            Rating = rating;
            Body = body;
            Url = url;
        }
    }
}
