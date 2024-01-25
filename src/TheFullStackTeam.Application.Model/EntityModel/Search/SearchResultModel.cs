namespace TheFullStackTeam.Application.Model.EntityModel.Search;

public class SearchResultModel
{
    public int TotalJobs { get; set; }
    public int TotalProfessionals { get; set; }
    public int TotalItems { get; set; }
    public IList<SearchResultItem> Results { get; set; }
    public int Page { get; set; }
}


