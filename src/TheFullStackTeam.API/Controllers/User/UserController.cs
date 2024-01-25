using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Model.POST;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Application.Users.Queries;

namespace TheFullStackTeam.API.Controllers.User
{
    /// <summary>
    /// User controller
    /// </summary>
    public class UserController : BaseController<UserController>
    {
        /// <summary>
        /// This region contains core mathods to management the logged user information
        /// </summary>
        #region
        /// <summary>
        /// Get a logged user profile 
        /// </summary>
        /// <returns></returns>
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
        /**
        /// <summary>
        /// Update basic information of user
        /// </summary>
        ///  <param name="model">User Basic Information model</param>
        /// <returns></returns>
        [HttpPut("me")]
        public async Task<IActionResult> Put([FromBody] UserModel model)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(accountId);
            var result = await Mediator.Send(new UpdateUserBasicInformationCommand(accountId, model));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        */
        /// <summary>
        /// Add contact information of user   
        /// </summary>
        /// /// <returns></returns>
        ///  
        [HttpPut("me/contact")]
        public async Task<IActionResult> PutContact([FromBody] ContactInformationModel informationEntity)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(new AddUserContactInformationCommand(accountId, informationEntity));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Update profile image of user   
        /// </summary>
        ///  <param name="model">File Post</param>
        [HttpPut("me/avatar")]
        public async Task<IActionResult> UploadAvetar([FromBody] FilePost model)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(new UploadUserAvatarCommand(accountId, model));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Get a profiles by logged User  
        /// </summary>
        /// <returns></returns>
        [HttpGet("me/profiles")]
        public async Task<IActionResult> GetProfiles()
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(new ReadUserProfilesQuery(accountId));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        #endregion

        /// <summary>
        /// This region contains core mathods to management general user cases about user entity
        /// </summary>
        #region
       
        /// <summary>
        /// Register User from Azure AD
        /// </summary>
        ///  <param name="model">User Model</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserModel model)
        {
            var result = await Mediator.Send(new RegisterUserCommand(model));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
               
        #endregion


    }
}
