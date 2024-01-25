using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class JobSalaryTypeListItem
{
    public Guid Id { get; set; }
    public string SalaryTypeName { get; set; }
    public double MinAmount { get; set; }
    public double MaxAmount { get; set; }
    public string Currency { get; set; } = null!;

    public static implicit operator JobSalaryTypeListItem(JobsSalaryType domainEntity) => new()
    {
        Id = domainEntity.Id,
        SalaryTypeName = domainEntity.SalaryTypeName,
        MinAmount= domainEntity.MinAmount,
        MaxAmount= domainEntity.MaxAmount,
        Currency = domainEntity.Currency,

    };

    public static Expression<Func<JobsSalaryType, JobSalaryTypeListItem>> Projection =>
        x => new JobSalaryTypeListItem
        {
            Id = x.Id,
            SalaryTypeName = x.SalaryTypeName,
            MinAmount= x.MinAmount,
            MaxAmount= x.MaxAmount,
            Currency = x.Currency
        };
}