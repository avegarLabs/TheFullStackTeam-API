using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class JobContractTypeListItem
{
    public Guid Id { get; set; }
    public string ContractTypeName { get; set; }


    public static implicit operator JobContractTypeListItem(JobContractType domainEntity) => new()
    {
        Id = domainEntity.Id,
        ContractTypeName = domainEntity.ContractTypeName
    };

    public static Expression<Func<JobContractType, JobContractTypeListItem>> Projection =>
        x => new JobContractTypeListItem
        {
            Id = x.Id,
            ContractTypeName = x.ContractTypeName

        };
}