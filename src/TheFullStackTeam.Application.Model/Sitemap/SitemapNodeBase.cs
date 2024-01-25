namespace TheFullStackTeam.Application.Model.Sitemap
{
    public abstract class SitemapNodeBase
    {
        public string Url { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
