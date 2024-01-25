using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Categories.Commands;
using TheFullStackTeam.Application.Categories.Queries;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Model.PUT;

namespace TheFullStackTeam.API.Controllers.Categories;

/// <summary>
/// Controller of Categories
/// </summary>
public class CategoryController : BaseController<CategoryController>
{
    /// <summary>
    /// Create a Category
    /// </summary>
    /// <param name="model">Category model</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryListItem), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] CategoryModel model)
    {
        var commandResponse = await Mediator.Send(new CreateCategoryCommand(model));
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryListItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var commandResponse = await Mediator.Send(new ReadCategoriesQuery());
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Update a Category
    /// </summary>
    /// <param name="model">Category model</param>
    /// <param name="id">Identifier</param>
    /// <response code="200">Item updated success</response>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CategoryListItem), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put([FromBody] CategoryPut model, Guid id)
    {
        var commandResponse = await Mediator.Send(new UpdateCategoryCommand(id, model));
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Delete Category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var commandResponse = await Mediator.Send(new DeleteCategoryCommand(id));
        return Ok(commandResponse.Success);
    }
}

