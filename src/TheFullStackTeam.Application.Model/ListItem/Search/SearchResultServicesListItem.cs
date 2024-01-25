using TheFullStackTeam.Application.Model.ValueObjects;

namespace TheFullStackTeam.Application.Model.ListItem.Search
{
    public class SearchResultServicesListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Moniker { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public List<string> Skills { get; set; }
        public List<string>  Categories { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }

    }
}
