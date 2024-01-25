using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class CategoryListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public static implicit operator CategoryListItem(Category domainEntity) => new()
    {
        Id = domainEntity.Id,
        Name = domainEntity.Name,
    };

    public static Expression<Func<Category, CategoryListItem>> Projection =>
        x => new CategoryListItem
        {
            Id = x.Id,
            Name = x.Name
        };
}
