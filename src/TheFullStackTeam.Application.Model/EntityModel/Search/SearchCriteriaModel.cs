namespace TheFullStackTeam.Application.Model.EntityModel.Search;

public class SearchCriteriaModel
{
    public List<string> JobTypes { get; set; }
    public List<string> ContractTypes { get; set; }
    public bool SalaryHour { get; set; }
    public bool SalaryMonth { get; set; }
    public bool SalaryYear { get; set; }
    public List<string> LanguageList { get; set; }
    public double HourSalaryMin { get; set; }
    public double HourSalaryMax { get; set; }
    public double MonthSalaryMin { get; set; }
    public double MonthSalaryMax { get; set; }
    public double YearSalaryMin { get; set; }
    public double YearSalaryMax { get; set; }

    public Guid? CountryId { get; set; }
    public List<string> Skills { get; set; }

    public PaginationModel Pagination { get; set; }
}



