using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class CityListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;


    public static implicit operator CityListItem(Cities domainEntity) => new()
    {
        Id = domainEntity.Id,
        Name = domainEntity.Name
    };

    public static Expression<Func<Cities, CityListItem>> Projection =>
        x => new CityListItem
        {
            Id = x.Id,
            Name = x.Name 
        };
}