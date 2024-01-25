using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel;

public class JobSalaryTypeModel
{
    public string SalaryTypeName { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public double MinAmount { get; set; }
    public double MaxAmount { get; set; }

    public static implicit operator JobsSalaryType(JobSalaryTypeModel model) => new()
    {
        SalaryTypeName = model.SalaryTypeName,
         MinAmount= model.MinAmount,
         MaxAmount= model.MaxAmount,
         Currency = model.Currency,
    };
}