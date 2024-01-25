using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.PUT;

public class CategoryPut : CategoryModel
{
    public static implicit operator Category(CategoryPut model)
    {
        return new Category
        {
            Name = model.Name
        };
    }
}
