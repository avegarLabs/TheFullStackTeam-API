using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Institution.Command;
using TheFullStackTeam.Application.Institution.Queries;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Skills.Commands;
using TheFullStackTeam.Application.Skills.Queries;

namespace TheFullStackTeam.API.Controllers.Admin
{


    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = "IsAdminOrCustomerService")]
    [Produces("application/json")]
    [Route("[area]/[Controller]")]
    public class InstitutionController: AdminBaseController
    {

        /// <summary>
        /// Get all Institutions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InstitutionListItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList()
        {
            var commandResponse = await Mediator.Send(new ListInstitutionQuery());
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Create a Institution
        /// </summary>
        /// <param name="model">Institution model</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(InstitutionListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] InstitutionModel model)
        {
            var commandResponse = await Mediator.Send(new CreateInstitutionCommand(model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a institution
        /// </summary>
        /// <param name="model">Institution model</param>
        /// <param name="Id">Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(InstitutionListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] InstitutionListItem model, Guid Id)
        {
            var commandResponse = await Mediator.Send(new UpdateInstitutionCommand(Id, model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete Institution
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var commandResponse = await Mediator.Send(new DeleteInstitutionCommand(Id));
            return Ok(commandResponse.Success);
        }
    }
}
