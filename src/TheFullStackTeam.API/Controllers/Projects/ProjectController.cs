using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Model.POST;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalProjects;
using TheFullStackTeam.Application.Professionals.Queries.ProfessionalProject;
//using TheFullStackTeam.Application.Projects.Commands;
//using TheFullStackTeam.Application.Projects.Queries;

namespace TheFullStackTeam.API.Controllers.Projects;

/// <summary>
/// TODO: Update summary.
/// </summary>
public class ProjectController : BaseController<ProjectController>
{
    /// <summary>
    /// TODO: Write a description for this method.
    /// </summary>
    /// <param name="moniker"></param>
    /// <returns></returns>
    [HttpGet("{moniker}")]
    [ProducesResponseType(typeof(ProjectListItem), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(string moniker)
    {
        var queryResponse = await Mediator.Send(new ReadProfessionalProjectByMonikerQuery(moniker));
        if (queryResponse.Success)
        {
            return Ok(queryResponse.Data);
        }

        return BadRequest();
    }

    /*
    /// <summary>
    /// TODO: Write a description for this method.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="moniker"></param>
    /// <returns></returns>
   /* [HttpPut("{moniker}")]
    public async Task<IActionResult> Put([FromBody] ProjectModel model, string moniker)
    {
        // var response = await Mediator.Send(new UpdateProjectCommand(moniker, model));
        //if (response.Success)
        //{
        //  return Ok(response.Data);
        //}

        //return BadRequest();
        return null;
    }*/

    /// <summary>
    /// TODO: Write a description for this method.
    /// </summary>
    /// <param name="projectMoniker"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("{projectMoniker}/[Action]")]
    public async Task<IActionResult> AddProjectTask(string projectMoniker, [FromBody] ProjectTaskPost model)
    {
        var response = await Mediator.Send(new CreateProfessionalProjectTaskCommand(projectMoniker, model));
        if (response.Success)
        {
            return Ok(response.Data);
        }

        return BadRequest();
    }
}