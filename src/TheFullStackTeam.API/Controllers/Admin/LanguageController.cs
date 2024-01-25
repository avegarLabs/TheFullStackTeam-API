using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Languages.Commands;
using TheFullStackTeam.Application.Languages.Queries;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.API.Controllers.Admin
{

    [Authorize(Policy = "IsAdminOrCustomerService")]
    [Produces("application/json")]
    [Route("[area]/[Controller]")]
    public class LanguageController: AdminBaseController
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

        /// <summary>
        /// Create a language
        /// </summary>
        /// <param name="model">Language model</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(LanguageListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] LanguageModel model)
        {
            var commandResponse = await Mediator.Send(new CreateLanguageCommand(model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a language
        /// </summary>
        /// <param name="model">Language model</param>
        /// <param name="moniker">Moniker</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(LanguageListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] LanguageListItem model, Guid Id)
        {
            var commandResponse = await Mediator.Send(new UpdateLanguageCommand(Id, model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete language
        /// </summary>
        /// <param name="LanguageId"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var commandResponse = await Mediator.Send(new DeleteLanguageCommand(Id));
            return Ok(commandResponse.Success);
        }
    }
}
