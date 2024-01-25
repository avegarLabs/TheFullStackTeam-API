using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using TheFullStackTeam.Application.Model.EntityModel.Search;
using TheFullStackTeam.Application.Search.Queries;

namespace TheFullStackTeam.API.Controllers.Search
{
    [AllowAnonymous]
    public class SearchController : BaseController<SearchController>
    {
         /// <summary>
        /// Search professionals and organizations by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<SearchResult>> ByName([FromQuery]string name)
        {
            var command = new SearchByNameQuery(name);
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Search professionals using query parameters
        /// </summary>
        /// <returns></returns>
        [HttpPost("prof")]
        public async Task<ActionResult<SearchResult>> ProfessionalByParameters([FromBody] SearchCriteriaModel QueryModel)
        {
            var command = new SearchProfessionalByParametersQuery(QueryModel);
            var result = await Mediator.Send(command);

            return Ok(result);

        }

        /// <summary>
        /// Search jobs using query parameters
        /// </summary>
        /// <returns></returns>
        [HttpPost("job")]
        public async Task<ActionResult<SearchResult>> JobByParameters([FromBody] SearchCriteriaModel QueryModel)
        {
            var command = new SearchJobByParametersQuery(QueryModel);
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Search services using query parameters
        /// </summary>
        /// <returns></returns>
        [HttpPost("services")]
        public async Task<ActionResult<SearchResult>> ServiceByParameters([FromBody] SearchServiceCriteriaModel QueryModel)
        {
            var command = new SearchServicesByParametersQuery(QueryModel);
            var result = await Mediator.Send(command);

            return Ok(result);

        }
    }
}
