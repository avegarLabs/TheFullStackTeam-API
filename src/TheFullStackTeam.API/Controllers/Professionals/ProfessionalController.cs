using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals;
using TheFullStackTeam.Application.Professionals.Commands;
using TheFullStackTeam.Application.Professionals.Commands.PaymentMethod;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalContractType;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalJobType;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalSalaryType;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Commands;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalLenguage.Queries;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalPositions;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalProjects;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalsPositions;
using TheFullStackTeam.Application.Professionals.Queries;


namespace TheFullStackTeam.API.Controllers.Professionals
{
    /// <summary>
    /// Professional controller
    /// </summary>
    public class ProfessionalController : BaseController<ProfessionalController>
    {
        /// <summary>
        /// This region contains a core methods to management professional entity CRUD Operation
        /// </summary>
        #region
        /// <summary>
        /// Get all professionals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProfessionalListItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> List()
        {
            var queryResponse = await Mediator.Send(new ListProfessionalQuery());
            if (queryResponse.Success)
            {
                return Ok(queryResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get professional by moniker
        /// </summary>
        /// <returns></returns>
        [HttpGet("{moniker}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProfessionalListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string moniker)
        {
            var queryResponse = await Mediator.Send(new ReadProfessionalByMonikerQuery(moniker));
            if (queryResponse.Success)
            {
                return Ok(queryResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get a active professional
        /// </summary>
        /// <returns></returns>
        [HttpGet("active")]
        [ProducesResponseType(typeof(ProfessionalListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Active()
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok((await Mediator.Send(new ReadActiveProfessionalQuery(accountId))).Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProfessionalListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] RegisterProfessionalModel model)
        {
            var commandResponse = await Mediator.Send(new ProfessionalRegisterCommand(model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a professional
        /// </summary>
        /// <param name="model">Professional model</param>
        /// <param name="ProfessionalId">ProfessionalId</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}")]
        [ProducesResponseType(typeof(ProfessionalListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] ProfessionalListItem model, Guid ProfessionalId)
        {
            var commandResponse = await Mediator.Send(new UpdateProfessionalCommand(ProfessionalId, model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete professional
        /// </summary>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpDelete("{ProfessionalId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid ProfessionalId)
        {
            var commandResponse = await Mediator.Send(new DeleteProfessionalCommand(ProfessionalId));
            return Ok(commandResponse.Success);
        }

        #endregion
        /// <summary>
        /// This region contains CRUD operation about professional Skills
        /// </summary>

        #region
        /// <summary>
        /// List professional skills
        /// </summary>
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/skills")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSkills(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ListProfessionalSkillsQuery(ProfessionalId))).Data);

        /// <summary>
        /// Add professional skill
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/skills")]
        public async Task<IActionResult> AddSkill([FromBody] ProfessionalSkillModel model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalSkillsCommand(model, ProfessionalId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Update a professional skill
        /// </summary>
        /// <param name="model">ProfessionalSkill model</param>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <param name="ProfessionalSkillId">ProfessionalSkill Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/skill/{ProfessionalSkillId}")]
        public async Task<IActionResult> UpdateSkill([FromBody] ProfessionalSkillModel model, Guid ProfessionalId, Guid ProfessionalSkillId)
        {
            var response = await Mediator.Send(new UpdateProfessionalSkillCommand(model, ProfessionalId, ProfessionalSkillId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete professional skill
        /// </summary>
        /// <param name="ProfessionalSkillId"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpDelete("{ProfessionalId}/skill/{professionalSkillId}")]
        public async Task<IActionResult> RemoveSkill(Guid ProfessionalId, Guid ProfessionalSkillId)
        {
            var result = await Mediator.Send(new DeleteProfessionalSkillCommand(ProfessionalId, ProfessionalSkillId));
            return Ok(result.Success);
        }

        #endregion

        #region
        /// <summary>
        /// This region contains CRUD operation about professional Position
        /// </summary>
        /// <summary>
        /// List professional positions
        /// </summary>
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/positions")]
        [AllowAnonymous]
        public async Task<IActionResult> Experiences(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ListProfessionalPositionQuery(ProfessionalId))).Data);


        /// <summary>
        /// Add professional position
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/position")]
        public async Task<IActionResult> AddExperience([FromBody] PositionModel model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalPositionCommand(model, ProfessionalId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Update a professional position
        /// </summary>
        /// <param name="model">Position Item model</param>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <param name="PositionId">Position Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/position/{PositionId}")]
        public async Task<IActionResult> UpdateExperience([FromBody] PositionModel model, Guid ProfessionalId, Guid PositionId)
        {
            var response = await Mediator.Send(new UpdateProfessionalPositionCommand(model, ProfessionalId, PositionId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a professional position add new skill
        /// </summary>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <param name="PositionId">Position Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/position/{PositionId}/skill")]
        public async Task<IActionResult> UpdatePositionAddSkill(Guid ProfessionalId, Guid PositionId, [FromBody] SkillModel model)
        {
            var response = await Mediator.Send(new AddSkillToProfessionalPositionCommand(ProfessionalId, PositionId, model));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a professional position remove skill
        /// </summary>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <param name="PositionId">Position Id</param>
        /// <param name="SkillId">Skill Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpDelete("{ProfessionalId}/position/{PositionId}/skill/{SkillId}")]
        public async Task<IActionResult> UpdatePositionDeleteSkill(Guid ProfessionalId, Guid PositionId, Guid SkillId)
        {
            var response = await Mediator.Send(new DeleteSkillInProfessionalPositionCommand(ProfessionalId, PositionId, SkillId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Delete professional position
        /// </summary>
        /// <param name="PositionId"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpDelete("{ProfessionalId}/position/{PositionId}")]
        public async Task<IActionResult> RemoveExperience(Guid ProfessionalId, Guid PositionId)
        {
            var result = await Mediator.Send(new DeleteProfessionalPositionCommand(ProfessionalId, PositionId));
            return Ok(result.Success);
        }
        #endregion
        #region
        /// <summary>
        /// This region contains CRUD operation about professional Title
        /// </summary>
        /// 
        /// <summary>
        /// List professional titles
        /// </summary>
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/titles")]
        [AllowAnonymous]
        public async Task<IActionResult> Titles(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ListProfessionalTitlesQuery(ProfessionalId))).Data);


        /// <summary>
        /// Add professional titles
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/titles")]
        public async Task<IActionResult> AddTitle([FromBody] TitleModel model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalTitleCommand(model, ProfessionalId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Update a professional title
        /// </summary>
        /// <param name="model">Title Item model</param>
        /// <param name="ProfessionalId">Professional Id</param>
        ///  <param name="TitleId">Title Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/titles/{TitleId}")]
        public async Task<IActionResult> UpdateTitle([FromBody] TitleModel model, Guid ProfessionalId, Guid TitleId)
        {
            var response = await Mediator.Send(new UpdateProfessionalTitleCommand(model, ProfessionalId, TitleId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Delete professional title
        /// </summary>
        /// <param name="TitleId"></param>
        /// /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpDelete("{ProfessionalId}/titles/{TitleId}")]
        public async Task<IActionResult> RemoveTitle(Guid ProfessionalId, Guid TitleId)
        {
            var result = await Mediator.Send(new DeleteProfessionalTitleCommand(ProfessionalId, TitleId));
            return Ok(result.Success);
        }

        #endregion

        /// <summary>
        /// This region contains CRUD operation about professional Honor
        /// </summary>
        #region
        /// <summary>
        /// List professional honors
        /// </summary>
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/honor")]
        public async Task<IActionResult> Honors(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ListProfessionalHonorsQuery(ProfessionalId))).Data);


        /// <summary>
        /// Add professional honor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/honor")]
        public async Task<IActionResult> AddHonor([FromBody] HonorModel model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalHonorCommand(model, ProfessionalId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Update a professional honor
        /// </summary>
        /// <param name="model">Honor model</param>
        /// <param name="ProfessionalId">Professional Id</param>
        /// /// <param name="HonorId">Honor Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/honor/{HonorId}")]
        public async Task<IActionResult> UpdateHonor([FromBody] HonorModel model, Guid ProfessionalId, Guid HonorId)
        {
            var response = await Mediator.Send(new UpdateProfessionalHonorCommand(model, ProfessionalId, HonorId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete professional honor
        /// </summary>
        /// <param name="HonorId"></param>
        /// <returns></returns>
        [HttpDelete("honor/{HonorId}")]
        public async Task<IActionResult> RemoveHonor(Guid HonorId)
        {
            var result = await Mediator.Send(new DeleteProfessionalHonorCommand(HonorId));
            return Ok(result.Success);
        }
        #endregion
        /// <summary>
        /// This region contains CRUD operation about professional Clients
        /// </summary>
        #region
        /// <summary>
        /// Get all professionals clients
        /// </summary>
        /// <param name="ProfessionalId">A professional Id</param>
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/clients")]
        public async Task<IActionResult> Clients(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ListProfessionalClientsQuery(ProfessionalId))).Data);

        /// <summary>
        /// Add a client to a professional
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/clients")]
        public async Task<IActionResult> AddClient([FromBody] ClientModel model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalClientCommand(model, ProfessionalId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a client of a professional
        /// </summary>
        /// <param name="model"></param>
        /// <param name=ProfessionalId"></param>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/clients/{ClientId}")]
        [ProducesResponseType(typeof(ClientListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateClient([FromBody] ClientModel model, Guid ProfessionalId, Guid ClientId)
        {
            var commandResponse = await Mediator.Send(new UpdateProfessionalClientCommand(ProfessionalId, ClientId, model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Delete a client of a professional
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        [HttpDelete("clients/{ClientId}")]
        public async Task<IActionResult> RemoveClient(Guid ClientId)
        {
            var result = await Mediator.Send(new DeleteProfessionalClientCommand(ClientId));
            return Ok(result.Success);
        }
        #endregion

        /// <summary>
        /// This region contains a core methods to management professional projects CRUD Operation
        /// </summary>


        #region

        /// <summary>
        /// TODO" Write a description for the GetProjects method.
        /// </summary>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/projects")]
        public async Task<IActionResult> Projects(Guid ProfessionalId)
        {
            var queryResponse = await Mediator.Send(new ReadProfessionalProjectsQuery(ProfessionalId));
            if (queryResponse.Success)
            {
                return Ok(queryResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// TODO: Write a description for this method.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/projects")]
        [ProducesResponseType(typeof(ProjectListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> Project([FromBody] ProjectModel model, Guid ProfessionalId)
        {
            var commandResponse = await Mediator.Send(new CreateProfessionalProjectCommand(model, ProfessionalId));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a project of a professional
        /// </summary>
        /// <param name="model"></param>
        /// <param name=ProfessionalId"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/projects/{ClientId}")]
        [ProducesResponseType(typeof(ProjectListItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectModel model, Guid ProfessionalId, Guid ProjectId)
        {
            var commandResponse = await Mediator.Send(new UpdateProfessionalProjectCommand(ProfessionalId, ProjectId, model));
            if (commandResponse.Success)
            {
                return Ok(commandResponse.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete a Project of a professional
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [HttpDelete("projects/{ProjectId}")]
        public async Task<IActionResult> RemoveProject(Guid ProjectId)
        {
            var result = await Mediator.Send(new DeleteProfessionalProjectCommand(ProjectId));
            return Ok(result.Success);
        }
        #endregion

        /// <summary>
        /// This region contains a core methods to management professional contracts CRUD Operation
        /// </summary>
        #region

        /// <summary>
        /// List professional contracts
        /// </summary>
        [HttpGet("{ProfessionalId}/ctype")]
        public async Task<IActionResult> Jobs(Guid ProfessionalId)
          => Ok((await Mediator.Send(new ListProfessionalContractsTypeQuery(ProfessionalId))).Data);


        /// <summary>
        /// Add professional Contracts
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/contracttype")]
        public async Task<IActionResult> AddContractsTypes([FromBody] ProfessionalContractTypeModel model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalContractTypeCommand(model, ProfessionalId));
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete professional Contracts
        /// </summary>
        /// <param name="ProfessionalContractId"></param>
        /// <returns></returns>
        [HttpDelete("contracttype/{ProfessionalContractId}")]
        public async Task<IActionResult> RemoveContracts(Guid ProfessionalContractId)
        {
            var result = await Mediator.Send(new DeleteProfessionalContractTypeCommand(ProfessionalContractId));
            return Ok(result.Success);
        }

        #endregion

        /// <summary>
        /// This region contains a core methods to management professional Job CRUD Operation
        /// </summary>
        #region
        /// <summary>
        /// List professional jobs
        /// </summary>
        [HttpGet("{ProfessionalId}/jobType")]
        public async Task<IActionResult> GetCtype(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ListProfessionalJobsTypeQuery(ProfessionalId))).Data);


        /// <summary>
        /// Add professional Jobs
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/jobType")]
        public async Task<IActionResult> AddJobs([FromBody] ProfessionalJobTypeModel model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalJobTypeCommand(model, ProfessionalId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Delete professional Job Type
        /// </summary>
        /// <param name="ProfessionalJobId"></param>
        /// <returns></returns>
        [HttpDelete("jobType/{ProfessionalJobId}")]
        public async Task<IActionResult> RemoveJob(Guid ProfessionalJobId)
        {
            var result = await Mediator.Send(new DeleteProfessionalJobTypeCommand(ProfessionalJobId));
            return Ok(result.Success);
        }

        #endregion

        /// <summary>
        /// This region contains a core methods to management professional services CRUD Operation
        /// </summary>
        /// 
        #region
        /// <summary>
        /// List professional services
        /// </summary>
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/services")]
        [AllowAnonymous]
        public async Task<IActionResult> Services(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ListProfessionalServicesQuery(ProfessionalId))).Data);


        /// <summary>
        /// Add professional services
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/services")]
        public async Task<IActionResult> AddServices([FromBody] ProfessionalServiceModel model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalServicesTypeCommand(model, ProfessionalId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Update a professional services
        /// </summary>
        /// <param name="model">ProfessionalServices model</param>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <param name="ServiceId">Services Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/services/{ServiceId}")]
        public async Task<IActionResult> UpdateServices([FromBody] ProfessionalServiceModel model, Guid ProfessionalId, Guid ServiceId)
        {
            var response = await Mediator.Send(new UpdateProfessionalServicesCommand(model, ProfessionalId, ServiceId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a professional services add new skill
        /// </summary>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <param name="ServiceId">Services Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/services/{ServiceId}/skill")]
        public async Task<IActionResult> UpdateServicesAddSkill(Guid ProfessionalId, Guid ServiceId, [FromBody] SkillModel model)
        {
            var response = await Mediator.Send(new AddSkillToProfessionalServicesCommand(ProfessionalId, ServiceId, model));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Update a professional services remove skill
        /// </summary>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <param name="ServiceId">Services Id</param>
        /// <param name="SkillId">Skill Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpDelete("{ProfessionalId}/services/{ServiceId}/skill/{SkillId}")]
        public async Task<IActionResult> UpdateServicesDeleteSkill(Guid ProfessionalId, Guid ServiceId, Guid SkillId)
        {
            var response = await Mediator.Send(new DeleteSkillInProfessionalServiceCommand(ProfessionalId, ServiceId, SkillId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete professional services
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <returns></returns>
        [HttpDelete("{ProfessionalId}/services/{ServiceId}")]
        public async Task<IActionResult> RemoveServices(Guid ProfessionalId, Guid ServiceId)
        {
            var result = await Mediator.Send(new DeleteProfessionalServicesCommand(ProfessionalId, ServiceId));
            return Ok(result.Success);
        }
        #endregion

        /// <summary>
        /// This region contains a core methods to management professional Salary CRUD Operation
        /// </summary>
        /// 
        #region

        /// <summary>
        /// List professional Salary Type
        /// </summary>
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/salary")]
        public async Task<IActionResult> Salary(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ListProfessionalSalaryTypeQuery(ProfessionalId))).Data);


        /// <summary>
        /// Add professional Salary Type
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/salary")]
        public async Task<IActionResult> AddSalary([FromBody] ProfessionalSalaryTypeModel model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalSalaryTypeCommand(model, ProfessionalId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Update a professional salary Type
        /// </summary>
        /// <param name="model">ProfessionalSalaryType model</param>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <param name="SalaryTypeId">Salary Type Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/salary/{SalaryTypeId}")]
        public async Task<IActionResult> UpdateSalary([FromBody] ProfessionalSalaryTypeModel model, Guid ProfessionalId, Guid SalaryTypeId)
        {
            var response = await Mediator.Send(new UpdateProfessionalSalaryTypeCommand(model, ProfessionalId, SalaryTypeId));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete professional salary type
        /// </summary>
        /// <param name="ProfessionalSalaryId"></param>
        /// <returns></returns>
        [HttpDelete("salary/{ProfessionalSalaryId}")]
        public async Task<IActionResult> RemoveSalary(Guid ProfessionalSalaryId)
        {
            var result = await Mediator.Send(new DeleteProfessionalSalaryTypeCommand(ProfessionalSalaryId));
            return Ok(result.Success);
        }

        #endregion

        /// <summary>
        /// This region contains a core methods to management professional payments methods CRUD Operation
        /// </summary>
        /// 
        #region
        /// <summary>
        /// Get a list with payments methods of user
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/paymentmethod")]
        public async Task<IActionResult> GetPaymentMethods(Guid ProfessionalId)
        {
            var result = await Mediator.Send(new ReadProfessionalPaymentMethodsQuery(ProfessionalId));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Add payment method of user
        /// </summary>
        ///  <param name="model">Payment Method model</param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/paymentmethod")]
        public async Task<IActionResult> PostPaymentMethod([FromBody] PaymentMethodModel model, Guid ProfessionalId)
        {
            var result = await Mediator.Send(new AddProfessionalPaymentMethodCommand(ProfessionalId, model));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }


        /// <summary>
        /// Update data about payment method of user
        /// </summary>
        ///  <param name="PaymentMethodId">PaymentMethodId</param>
        ///  <param name="model">Payment Method model</param>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/paymentmethod/{PaymentMethodId}")]
        public async Task<IActionResult> PutPaymentMethod([FromBody] PaymentMethodModel model, Guid PaymentMethodId, Guid ProfessionalId)
        {
            var result = await Mediator.Send(new UpdateProfessionalPaymentMethodCommand(ProfessionalId, PaymentMethodId, model));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete payment method of user
        /// </summary>
        ///  <param name="PaymentMethodId">PaymentMethodId</param>
        ///  <param name="ProfessionalId">ProfessionalId</param>
        /// <returns></returns>
        [HttpDelete("{ProfessionalId}/paymentmethod/{PaymentMethodId}")]
        public async Task<IActionResult> DeletePaymentMethod(Guid ProfessionalId, Guid PaymentMethodId)
        {
            var result = await Mediator.Send(new DeleteProfessionalMethodUserCommand(ProfessionalId, PaymentMethodId));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        #endregion


        /// <summary>
        /// This region contains a core methods to management professional Language  CRUD Operation
        /// </summary>
        /// 
        #region
        /// <summary>
        /// List professional language
        /// </summary>
        /// <returns></returns>
        [HttpGet("{ProfessionalId}/language")]
        public async Task<IActionResult> GetLanguege(Guid ProfessionalId)
            => Ok((await Mediator.Send(new ProfessionalLanguegeQuery(ProfessionalId))).Data);

        /// <summary>
        /// Add professional skill
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpPost("{ProfessionalId}/language")]
        public async Task<IActionResult> AddLanguege([FromBody] ProfessionalLanguegeModel Model, Guid ProfessionalId)
        {
            var response = await Mediator.Send(new CreateProfessionalLanguegeCommand(ProfessionalId, Model));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }


        /// <summary>
        /// Update a professional Language
        /// </summary>
        /// <param name="model">ProfessionalLanguegeListItem model</param>
        /// <param name="ProfessionalId">Professional Id</param>
        /// <param name="ProfessionalLanguegeId">Professional Language Id</param>
        /// <response code="200">Item updated success</response>
        /// <returns></returns>
        [HttpPut("{ProfessionalId}/language/{ProfessionalLanguegeId}")]
        public async Task<IActionResult> UpdateLanguege([FromBody] ProfessionalLanguegeListItem model, Guid ProfessionalId, Guid ProfessionalLanguegeId)
        {
            var response = await Mediator.Send(new UpdateProfessionalLanguageCommand(ProfessionalId, ProfessionalLanguegeId, model));
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete professional Language
        /// </summary>
        /// <param name="ProfessionalLanguegeId"></param>
        /// <param name="ProfessionalId"></param>
        /// <returns></returns>
        [HttpDelete("{ProfessionalId}/language/{ProfessionalLanguegeId}")]
        public async Task<IActionResult> RemoveLanguege(Guid ProfessionalId, Guid ProfessionalLanguegeId)
        {
            var result = await Mediator.Send(new DeleteProfessionalLanguegeCommand(ProfessionalId, ProfessionalLanguegeId));
            return Ok(result.Success);
        }

        #endregion
    }


}
