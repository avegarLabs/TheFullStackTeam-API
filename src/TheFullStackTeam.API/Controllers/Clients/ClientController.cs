using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Clients.Queries;
using TheFullStackTeam.Application.Model.GET;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.API.Controllers.Clients;

/// <summary>
/// Controller of client
/// </summary>
public class ClientController : BaseController<ClientController>
{
    /// <summary>
    /// Retrieve all clients
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientLookup>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var queryResponse = await Mediator.Send(new ReadClientsQuery());
        if (queryResponse.Success)
        {
            return Ok(queryResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// TODO: Write a description for this method.
    /// </summary>
    /// <param name="moniker"></param>
    /// <returns></returns>
    [HttpGet("{moniker}")]
    [ProducesResponseType(typeof(ClientListItem), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(string moniker)
    {
        var queryResponse = await Mediator.Send(new ReadClientsDetailQuery(moniker));
        if (queryResponse.Success)
        {
            return Ok(queryResponse.Data);
        }

        return BadRequest();
    }
}