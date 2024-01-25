using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Languages.Commands;
using TheFullStackTeam.Application.Languages.Queries;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.API.Controllers.Language
{

    /// <summary>
    /// Controller of Language
    /// </summary>
    public class LanguageController: BaseController<LanguageController>
    {
        /// <summary>
        /// Get all language
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LanguageListItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var commandResponse = await Mediator.Send(new ListLanguageQuery());
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }
      
    }
}
