using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Model.POST;
using TheFullStackTeam.Application.Organizations.Command;
using TheFullStackTeam.Application.Organizations.Commands;
using TheFullStackTeam.Application.Organizations.Commands.OrganizationServices;
using TheFullStackTeam.Application.Organizations.Commands.OrganizationsServices;
using TheFullStackTeam.Application.Organizations.Queries;
using TheFullStackTeam.Application.Organizations.Queries.OrganizationServices;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;

namespace TheFullStackTeam.API.Controllers.Organization;

/// <summary>
/// Organization controller
/// </summary>
public class OrganizationController : BaseController<OrganizationController>
{
    /// <summary>
    /// Get all organizations
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrganizationListItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var commandResponse = await Mediator.Send(new ReadOrganizationsQuery());
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    [HttpGet("suggestions")]
    [ProducesResponseType(typeof(IEnumerable<SugestionListItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList()
    {
        var commandResponse = await Mediator.Send(new OrganizationSugestionQuery());
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Create an organization
    /// </summary>
    /// <param name="model">Organization model</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrganizationListItem), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] OrganizationModel model)
    {
        var commandResponse = await Mediator.Send(new CreateOrganizationCommand(model));
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Update an organization
    /// </summary>
    /// <param name="model">Organization model</param>
    /// <param name="id">Moniker</param>
    /// <response code="200">Item updated success</response>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(OrganizationListItem), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put([FromBody] OrganizationModel model, Guid id)
    {
        var commandResponse = await Mediator.Send(new UpdateOrganizationCommand(id, model));
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Delete Organization
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var commandResponse = await Mediator.Send(new DeleteOrganizationCommand(id));
        return Ok(commandResponse.Success);
    }

    /// <summary>
    /// get organization by moniker
    /// </summary>
    /// <param name="model">Organization model</param>
    /// <param name="moniker">Moniker</param>
    /// <response code="200">Item updated success</response>
    /// <returns></returns>
    [HttpGet("{moniker}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(OrganizationListItem), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetail(string Moniker)
    {
        var commandResponse = await Mediator.Send(new ReadOrganizationDetailsQuery(Moniker));
        if (commandResponse.Success)
        {
            return Ok(commandResponse.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Update Logo in Organization   
    /// </summary>
    ///  <param name="OrganizationId">Organization Id</param>
    ///  <param name="model">File Post</param>
    [HttpPut("{OrganizationId}/logo")]
    public async Task<IActionResult> UploadLogo([FromBody] FilePost model, Guid OrganizationId)
    {
        var result = await Mediator.Send(new UploadOrganizationLogoCommand(OrganizationId, model));
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest();
    }


    /// <summary>
    /// This region contains a core methods to management organization services CRUD Operation
    /// </summary>
    /// 
    #region
    /// <summary>
    /// List organization services
    /// </summary>
    /// <returns></returns>
    [HttpGet("{OrganizationId}/services")]
    [AllowAnonymous]
    public async Task<IActionResult> Services(Guid OrganizationId)
        => Ok((await Mediator.Send(new ListOrganizationServicesQuery(OrganizationId))).Data);


    /// <summary>
    /// Add Organization services
    /// </summary>
    /// <param name="model"></param>
    /// <param name="OrganizationId"></param>
    /// <returns></returns>
    [HttpPost("{OrganizationId}/services")]
    public async Task<IActionResult> AddServices([FromBody] OrganizationServiceModel model, Guid OrganizationId)
    {
        var response = await Mediator.Send(new CreateOrganizationServicesTypeCommand(model, OrganizationId));
        if (response.Success)
        {
            return Ok(response.Data);
        }

        return BadRequest();
    }


    /// <summary>
    /// Update a organization services
    /// </summary>
    /// <param name="model">Organization Services model</param>
    /// <param name="OrganizationId">Organization Id</param>
    /// <param name="ServiceId">Services Id</param>
    /// <response code="200">Item updated success</response>
    /// <returns></returns>
    [HttpPut("{OrganizationId}/services/{ServiceId}")]
    public async Task<IActionResult> UpdateServices([FromBody] OrganizationServiceModel model, Guid OrganizationId, Guid ServiceId)
    {
        var response = await Mediator.Send(new UpdateOrganizationServicesCommand(model, OrganizationId, ServiceId));
        if (response.Success)
        {
            return Ok(response.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Update a organization services add new skill
    /// </summary>
    /// <param name="OrganizationId">Organization Id</param>
    /// <param name="ServiceId">Services Id</param>
    /// <response code="200">Item updated success</response>
    /// <returns></returns>
    [HttpPost("{OrganizationId}/services/{ServiceId}/skill")]
    public async Task<IActionResult> UpdateServicesAddSkill(Guid OrganizationId, Guid ServiceId, [FromBody] SkillModel model)
    {
        var response = await Mediator.Send(new AddSkillToOrganizationServicesCommand(OrganizationId, ServiceId, model));
        if (response.Success)
        {
            return Ok(response.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Update Organization services remove skill
    /// </summary>
    /// <param name="OrganizationId">Organization Id</param>
    /// <param name="ServiceId">Services Id</param>
    /// <param name="SkillId">Skill Id</param>
    /// <response code="200">Item updated success</response>
    /// <returns></returns>
    [HttpDelete("{OrganizationId}/services/{ServiceId}/skill/{SkillId}")]
    public async Task<IActionResult> UpdateServicesDeleteSkill(Guid OrganizationId, Guid ServiceId, Guid SkillId)
    {
        var response = await Mediator.Send(new DeleteSkillInOrganizationServiceCommand(OrganizationId, ServiceId, SkillId));
        if (response.Success)
        {
            return Ok(response.Data);
        }

        return BadRequest();
    }

    /// <summary>
    /// Delete organization services
    /// </summary>
    /// <param name="ServiceId"></param>
    /// <param name="OrganizationId">Organization Id</param>
    /// <returns></returns>
    [HttpDelete("{OrganizationId}/services/{ServiceId}")]
    public async Task<IActionResult> RemoveServices(Guid OrganizationId, Guid ServiceId)
    {
        var result = await Mediator.Send(new DeleteOrganizationServicesCommand(OrganizationId, ServiceId));
        return Ok(result.Success);
    }
    #endregion

   }