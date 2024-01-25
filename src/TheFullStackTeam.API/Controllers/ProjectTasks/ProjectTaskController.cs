using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Model.GET;
using TheFullStackTeam.Application.Professionals.Queries.ProfessionalProject;


namespace TheFullStackTeam.API.Controllers.Tasks;

/// <summary>
/// TODO: Add a descriptive summary to your class.
/// </summary>
public class ProjectTaskController : BaseController<ProjectTaskController>
{
    /// <summary>
    /// TODO: Add a descriptive summary to your method.
    /// </summary>
    /// <returns></returns>
    [HttpGet("{projectMoniker}")]
    [ProducesResponseType(typeof(IEnumerable<ProjectTaskGet>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(string projectMoniker)
    {
        var queryResponse = await Mediator.Send(new ReadProfessionalProjectTasksQuery(projectMoniker));
        if (queryResponse.Success)
        {
            return Ok(queryResponse.Data);
        }

        return BadRequest();
    }
}