using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.UserRoles.Commands;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;

namespace TheFullStackTeam.API.Controllers.Admin
{

    /// <summary>
    /// The main goal of this controller is manage the features of the User entity in the Admin Site 
    /// </summary>
   [Authorize(Policy = "IsAdminOrCustomerService")]
    [Produces("application/json")]
    [Route("[area]/[Controller]")]
    public class UserController: AdminBaseController
    {

        [HttpGet("me")]
        public async Task<IActionResult> Get()
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(new ReadUserProfileInformationQuery(accountId));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// List of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await Mediator.Send(new ListUserProfilesQueries());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Add new user
        /// </summary>
        ///  <param name="model">User Model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserModel model)
        {
            var result = await Mediator.Send(new CreateUserCommand(model));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="UserId">Professional Id</param>
        /// <returns></returns>
        [HttpDelete("{UserId}")]
        public async Task<IActionResult> RemoveUser(Guid UserId)
        {
            var result = await Mediator.Send(new DeleteUserProfileCommand(UserId));
            return Ok(result.Success);
        }

        /// <summary>
        /// Update User profile
        /// </summary>
        /// <param name="model">User model</param>
        /// <param name="UserId">User Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{UserId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserListItem model)
        {
            var response = await Mediator.Send(new UpdateUserProfileCommand(model));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// User Roles management
        /// </summary>
        #region
        /// <summary>
        /// Add Role to User
        /// </summary>
        /// <param name="RoleListItem"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPost("{UserId}/role")]
        public async Task<IActionResult> AddSkill([FromBody] RolesListItem model, Guid UserId)
        {
            var response = await Mediator.Send(new CreateUserRolesCommands(UserId, model));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete Role in User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpDelete("{UserId}/role/{RoleId}")]
        public async Task<IActionResult> RemoveSkill(Guid UserId, Guid RoleId)
        {
            var result = await Mediator.Send(new DeleteUserRolesCommands(UserId, RoleId));
            return Ok(result.Success);
        }

        #endregion

    }
}
