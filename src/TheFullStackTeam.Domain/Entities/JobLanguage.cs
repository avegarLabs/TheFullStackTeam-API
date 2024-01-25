namespace TheFullStackTeam.Domain.Entities
{
    public class JobLanguage
    {
        public Guid JobId { get; set; }
        public virtual Job Job { get; set; }

        public Guid LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
