using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Model.PUT;
using TheFullStackTeam.Application.Skills.Commands;
using TheFullStackTeam.Application.Skills.Queries;

namespace TheFullStackTeam.API.Controllers.Skills;

/// <summary>
/// Controller of skills
/// </summary>
public class SkillController : BaseController<SkillController>
{
    /// <summary>
    /// Get all skills
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SkillListItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var commandResponse = await Mediator.Send(new ReadSkillsQuery());
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Create a skill
    /// </summary>
    /// <param name="model">Skill model</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(SkillListItem), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] SkillModel model)
    {
        var commandResponse = await Mediator.Send(new CreateSkillCommand(model));
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Update a skill
    /// </summary>
    /// <param name="model">Skill model</param>
    /// <param name="moniker">Moniker</param>
    /// <response code="200">Item updated success</response>
    /// <returns></returns>
    [HttpPut("{Id}")]
    [ProducesResponseType(typeof(SkillListItem), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put([FromBody] SkillListItem model, Guid Id)
    {
        var commandResponse = await Mediator.Send(new UpdateSkillCommand(Id, model));
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Delete Skill
    /// </summary>
    /// <param name="moniker"></param>
    /// <returns></returns>
    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var commandResponse = await Mediator.Send(new DeleteSkillCommand(Id));
        return Ok(commandResponse.Success);
    }
}