using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel;

public class CategoryModel
{
    public string Name { get; set; } = null!;

    public static implicit operator Category(CategoryModel model) => new()
    {
        Name = model.Name,
    };
}
