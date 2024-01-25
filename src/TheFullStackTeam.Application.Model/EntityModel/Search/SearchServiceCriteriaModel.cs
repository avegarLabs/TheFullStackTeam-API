namespace TheFullStackTeam.Application.Model.EntityModel.Search;

public class SearchServiceCriteriaModel
{
    public double ServicePriceMin { get; set; }
    public double ServicePriceMax { get; set; }
    public List<string> Categoties { get; set; }
    public List<string> Skills { get; set; }
    public PaginationModel Pagination { get; set; }

}
