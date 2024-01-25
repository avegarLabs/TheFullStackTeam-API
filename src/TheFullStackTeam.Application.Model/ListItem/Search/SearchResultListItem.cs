using TheFullStackTeam.Application.Model.ValueObjects;

namespace TheFullStackTeam.Application.Model.ListItem.Search
{
    public class SearchResultListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Moniker { get; set; }
        public string Description { get; set; }
        public string ItemURL { get; set; }
        public string Owner { get; set; }
        public string Specifications { get; set; }
        public string Picture { get; set; }
        public string Type { get; set; }

    }
}
