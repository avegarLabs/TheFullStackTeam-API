using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Countries.Commands;
using TheFullStackTeam.Application.Countries.Queries;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.API.Controllers.Admin
{

    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = "IsAdminOrCustomerService")]
    [Produces("application/json")]
    [Route("[area]/[Controller]")]
    public class CountryController : AdminBaseController
    {
        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CountryListItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var commandResponse = await Mediator.Send(new ReadCountriesQuery());
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Create a Country
        /// </summary>
        /// <param name="model">Country model</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CountryModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CountryModel model)
        {
            var commandResponse = await Mediator.Send(new CreateCountryCommand(model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a Country
        /// </summary>
        /// <param name="model">Country model</param>
        /// <param name="id">Identifier</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CountryListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] CountryListItem model, Guid id)
        {
            var commandResponse = await Mediator.Send(new UpdateCountryCommand(id, model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete Country
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var commandResponse = await Mediator.Send(new DeleteCountryCommand(id));
            return Ok(commandResponse.Success);
        }
    }
}