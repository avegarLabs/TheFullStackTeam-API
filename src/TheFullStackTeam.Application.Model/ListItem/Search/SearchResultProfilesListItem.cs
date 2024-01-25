using TheFullStackTeam.Application.Model.ValueObjects;

namespace TheFullStackTeam.Application.Model.ListItem.Search
{
    public class SearchResultProfilesListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Moniker { get; set; }
        public string Title { get; set; }
        public CountryListItem Country { get; set; }
        public List<string> ServicesName { get; set; }
        public string Picture { get; set; }
        public string ItemURL { get; set; }
        public string Type { get; set; }
       
    }
}
