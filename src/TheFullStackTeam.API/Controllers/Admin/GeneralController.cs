using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.API.Controllers.Admin;
using TheFullStackTeam.Application.General.Command;
using TheFullStackTeam.Application.General.Queries;
using TheFullStackTeam.Application.General.Results;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.API.Controllers.General
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = "IsAdminOrCustomerService")]
    [Produces("application/json")]
    [Route("[area]/[Controller]")]
    public class GeneralController: AdminBaseController
    {
        /// <summary>
        /// Get basic stats about main entities in the TFST domain
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BasicEntityStatListItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var queryResponse = await Mediator.Send(new ListBasicEntityStatQuery());
            if (queryResponse.Success)
            {
                return Ok(queryResponse.Data);
            }

            return BadRequest();
        }
        [HttpGet("etl/countries")]
        [ProducesResponseType(typeof(IEnumerable<ETLCommandResults>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ETLCountries()
        {
            var queryResponse = await Mediator.Send(new ETLCountriesCommand());
            if (queryResponse.Success)
            {
                return Ok(queryResponse.Data);
            }

            return BadRequest();
        }

        [HttpGet("etl/countries/cities")]
        [ProducesResponseType(typeof(IEnumerable<ETLCommandResults>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ETLCities()
        {
            var queryResponse = await Mediator.Send(new ETLCountriesAndCitiesCommand());
            if (queryResponse.Success)
            {
                return Ok(queryResponse.Data);
            }

            return BadRequest();
        }
    }
}
