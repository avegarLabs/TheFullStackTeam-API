using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheFullStackTeam.Application.Jobs.Command;
using TheFullStackTeam.Application.Jobs.JobResponsability.Command;
using TheFullStackTeam.Application.Jobs.JobSkill.Command;
using TheFullStackTeam.Application.Jobs.Queries;
using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.API.Controllers.Job
{
    public partial class JobController : BaseController<JobController>
    {
        /// <summary>
        /// Add Job 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> AddJob([FromBody] JobModel Model)
        {
            var response = await Mediator.Send(new CreateJobsCommand(Model));
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Get Jobs Detail by Moniker
        /// </summary>
        /// <param name="Moniker"></param>
        /// <returns></returns>
        [HttpGet("{Moniker}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string Moniker)
            => Ok((await Mediator.Send(new ReadJobDetailQuery(Moniker))).Data);


        /// <summary>
        /// List Jobs post by professional profile
        /// </summary>
        /// <returns></returns>
        [HttpGet("pro/{ProfessionalId}")]
        public async Task<IActionResult> JobProfessionalList(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ListJobsProfessionalQuery(ProfessionalId))).Data);


        /// <summary>
        /// List Jobs post by organization profile
        /// </summary>
        /// <returns></returns>
        [HttpGet("org/{OrganizationId}")]
        public async Task<IActionResult> JobOrganizationList(Guid OrganizationId)
            => Ok((await Mediator.Send(new ListJobsOrganizationQuery(OrganizationId))).Data);

        /// <summary>
        /// Delete Job
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        [HttpDelete("{JobId}")]
        public async Task<IActionResult> RemoveJob(Guid JobId)
        {
            var result = await Mediator.Send(new DeleteJobCommand(JobId));
            return Ok(result.Success);
        }

        /// <summary>
        /// Update a Job
        /// </summary>
        /// <param name="model">Job Model</param>
        /// <param name="JobId">JobId</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{JobId}")]
        public async Task<IActionResult> UpdateJob([FromBody] JobModel model, Guid JobId)
        {
            var response = await Mediator.Send(new UpdateJobCommand(JobId, model));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }
        /// <summary>
        /// Update a Job
        /// </summary>
        /// <param name="State">Job State</param>
        /// <param name="JobId">JobId</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{JobId}/state/{State}")]
        public async Task<IActionResult> UpdateJobState(Guid JobId, bool State)
        {
            var response = await Mediator.Send(new UpdateJobAviabilityStateCommand(JobId, State));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// List of Job by active User
        /// </summary>
        /// <returns></returns>
        [HttpGet("me")]
        public async Task<IActionResult> GetJobsInUser()
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await Mediator.Send(new ListJobsInUserActiveQuery(accountId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }
           


        /// <summary>
        /// Job Responsibilities Operations
        /// </summary>
        #region
        /// <summary>
        /// Add Responsibilities in Job
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        [HttpPost("{JobId}/responsibility")]
        public async Task<IActionResult> AddResponsability(Guid JobId, JobResposabilitiesModel JobResposabilities)
        {
            var result = await Mediator.Send(new CreateJobResponsabilitiesCommand(JobId, JobResposabilities));
            return Ok(result.Success);
        }

        /// <summary>
        /// Update Responsibilities in Job
        /// </summary>
        /// <param name="JobResponsabilityId"></param>
        /// <param name="JobId"></param>
        /// <returns></returns>
        [HttpPut("{JobId}/responsibility/{JobResponsabilityId}")]
        public async Task<IActionResult> UpdateResponsability(Guid JobId, Guid JobResponsabilityId, JobResposabilitiesModel JobResposabilities)
        {
            var result = await Mediator.Send(new UpdateJobResponsabilitiesCommand(JobId, JobResponsabilityId, JobResposabilities));
            return Ok(result.Success);
        }

        /// <summary>
        /// Delete Responsibilities in Job
        /// </summary>
        /// <param name="JobResponsabilityId"></param>
        /// <param name="JobId"></param>
        /// <returns></returns>
        [HttpDelete("responsibility/{JobResponsabilityId}")]
        public async Task<IActionResult> RemoveResponsability(Guid JobResponsabilityId)
        {
            var result = await Mediator.Send(new DeleteJobResponsabilitiesCommand(JobResponsabilityId));
            return Ok(result.Success);
        }



        #endregion
        /// <summary>
        /// Job Skill Operations
        /// </summary>
        #region
        /// <summary>
        /// Add Skill in Job
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        [HttpPost("{JobId}/skill")]
        public async Task<IActionResult> AddSkill(Guid JobId, JobSkillModel JobSkillModel)
        {
            var result = await Mediator.Send(new CreateJobSkillCommand(JobId, JobSkillModel));
            return Ok(result.Success);
        }

            /// <summary>
            /// Update Skill in Job
            /// </summary>
            /// <param name="JobSkillId"></param>
            /// <param name="JobId"></param>
            /// <returns></returns>
            [HttpPut("{JobId}/skill/{JobSkillId}")]
            public async Task<IActionResult> UpdateSkill(Guid JobId, Guid JobSkillId, JobSkillModel JobSkillModel)
            {
                var result = await Mediator.Send(new UpdateJobSkillCommand(JobId, JobSkillId, JobSkillModel));
                return Ok(result.Success);
            }


            /// <summary>
            /// Delete Skill in Job
            /// </summary>
            /// <param name="SkillId"></param>
            /// <param name="JobId"></param>
            /// <returns></returns>
            [HttpDelete("skill/{SkillId}")]
        public async Task<IActionResult> RemoveSkill(Guid SkillId)
        {
            var result = await Mediator.Send(new DeleteJobSkillCommand(SkillId));
            return Ok(result.Success);
        }
        #endregion
       

        
    }
}
