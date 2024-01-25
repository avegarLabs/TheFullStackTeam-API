using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Roles.Commands;
using TheFullStackTeam.Application.Roles.Queries;
using TheFullStackTeam.Application.Skills.Commands;
using TheFullStackTeam.Application.Users.Queries;

namespace TheFullStackTeam.API.Controllers.Admin
{

    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = "IsAdminOrCustomerService")]
    [Produces("application/json")]
    [Route("[area]/[Controller]")]
    public class RolesController: AdminBaseController
    {

         /// <summary>
        /// List of Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new ListRolesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Create a Role
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(RolesListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] RolesModel model)
        {
            var commandResponse = await Mediator.Send(new CreateRolesCommands(model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a role
        /// </summary>
        /// <param name="model">Role model</param>
        /// <param name="RoleId">Role Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(RolesListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] RolesListItem model, Guid Id)
        {
            var commandResponse = await Mediator.Send(new UpdateRoleCommand(Id, model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="Role Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var commandResponse = await Mediator.Send(new DeleteRolesCommands(Id));
            return Ok(commandResponse.Success);
        }

    }
}
