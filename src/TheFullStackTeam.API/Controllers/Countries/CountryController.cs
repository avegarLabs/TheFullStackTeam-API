using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Cities.Queries;
using TheFullStackTeam.Application.Countries.Queries;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.API.Controllers.Countries;

/// <summary>
/// Controller of countries
/// </summary>
public class CountryController : BaseController<CountryController>
{
    /// <summary>
    /// Get all countries
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
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
    /// Get all cities in country
    /// <param name="CountrId"></param>
    /// </summary>
    /// <returns></returns>
    [HttpGet("{CountrId}/cities")]
    [ProducesResponseType(typeof(IEnumerable<CityListItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCities(Guid CountrId)
    {
        var commandResponse = await Mediator.Send(new ListCitiesByCountryQuery(CountrId));
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }


}