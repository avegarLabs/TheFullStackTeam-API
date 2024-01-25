using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Jobs.Command;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.Handler
{
    public class CreatedJobCommandHandler : AppRequestHandler, IRequestHandler<CreateJobsCommand, CreateJobCommandResult>
    {
        private readonly IMonikerService _moniker;

        public CreatedJobCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _moniker = moniker;
        }

        public async Task<CreateJobCommandResult> Handle(CreateJobsCommand request, CancellationToken cancellationToken)
        {
            var country = await _context.Countries.Where(c => c.Id.Equals(request.Model.CountryId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            var jobs = new Job()
            {
                JobTitle = request.Model.JobTitle,
                JobDescription = request.Model.JobDescription,
                Moniker = await _moniker.FindValidMoniker<Job>(request.Model.JobTitle),
                CountryId = country.Id,
                Active = true
            };

            if (request.Model.Type == 1)
            {
                jobs.OrganizationId = request.Model.OwnerId;
            }
            else
            {
                jobs.ProfessionalId = request.Model.OwnerId;
            }
            _context.Add(jobs);
            if (request.Model.LanguagesRequired.Count() > 0)
            {
                jobs.RequiredLanguages = GetLanguegeList(jobs, request.Model.LanguagesRequired);
            }
            await AddSkillToJob(request.Model.JobSkills, jobs, cancellationToken);
            await AddContractsToJob(request.Model.JobContractTypes, jobs, cancellationToken);
            await AddLocationToJob(request.Model.JobsJobTypes, jobs, cancellationToken);
            await AddSalaryToJob(request.Model.JobsSalaryTypes, jobs, cancellationToken);
            await AddResponsabilitiesToJob(request.Model.JobResponsabilities, jobs, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return new CreateJobCommandResult(jobs);
        }

        private async Task AddResponsabilitiesToJob(List<JobResposabilitiesModel> jobResponsabilities, Domain.Entities.Job jobs, CancellationToken cancellationToken)
        {
            List<JobResponsabilities> responsabilitiesList = new List<JobResponsabilities>();


            foreach (JobResposabilitiesModel item in jobResponsabilities)
            {
                JobResponsabilities responsabilities = item;
                responsabilities.JobId = jobs.Id;
                responsabilitiesList.Add(responsabilities);
            }
            await _context.JobResponsabilities.AddRangeAsync(responsabilitiesList, cancellationToken);
        }

        private async Task AddSalaryToJob(JobSalaryTypeModel salaryModel, Job jobs, CancellationToken cancellationToken)
        {
            var jobSalary = new JobsSalaryType
            {
                SalaryTypeName = salaryModel.SalaryTypeName,
                MinAmount = salaryModel.MinAmount,
                MaxAmount = salaryModel.MaxAmount,
                Currency = salaryModel.Currency,
                JobId = jobs.Id,
            };

            await _context.jobsSalaryTypes.AddAsync(jobSalary, cancellationToken);
        }

        private async Task AddSkillToJob(List<JobSkillModel> jobSkills, Job jobs, CancellationToken cancellationToken)
        {
            foreach (JobSkillModel item in jobSkills)
            {
                var skill = await _context.Skills
                    .Where(s => s.Moniker.ToLower().Equals(item.SkillMoniker.ToLower().Trim()))
                    .SingleOrDefaultAsync(cancellationToken);

                Domain.Entities.JobSkill jobSkill = item;
                jobSkill.JobId = jobs.Id;

                if (skill == null)
                {
                    skill = new Skill()
                    {
                        Moniker = await _moniker.FindValidMoniker<Skill>(item.SkillName.Trim()),
                        Name = item.SkillName,
                        JobSkills = new List<Domain.Entities.JobSkill> { jobSkill }
                    };
                    await _context.Skills.AddAsync(skill, cancellationToken);
                }
                else
                {
                    skill.JobSkills.Add(jobSkill);
                    _context.Skills.Update(skill);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task AddLocationToJob(List<JobJobTypeModel> jobsJobTypes, Domain.Entities.Job jobs, CancellationToken cancellationToken)
        {
            List<JobsJobType> jobTypeForms = jobsJobTypes
                .Select(item => new JobsJobType
                {
                    JobTypeName = item.JobTypeName,
                    JobId = jobs.Id,
                })
                .ToList();

            await _context.JobsJobTypes.AddRangeAsync(jobTypeForms, cancellationToken);

        }

        private async Task AddContractsToJob(List<JobContractTypeModel> jobContractTypes, Domain.Entities.Job jobs, CancellationToken cancellationToken)
        {
            List<JobContractType> jobContracts = jobContractTypes
                .Select(item => new JobContractType
                {
                    ContractTypeName = item.ContractTypeName,
                    JobId = jobs.Id,
                })
                .ToList();

            await _context.JobContractTypes.AddRangeAsync(jobContracts, cancellationToken);

        }


        private ICollection<JobLanguage> GetLanguegeList(Job job, List<string> languagesRequired)
        {
            List<JobLanguage> languages = new List<JobLanguage>();
            foreach (string language in languagesRequired)
            {
                var lang = _context.Languages.Where(l => l.Name.Equals(language)).SingleOrDefault();
                var jobLang = new JobLanguage()
                {
                    JobId = job.Id,
                    LanguageId = lang.Id
                };
                languages.Add(jobLang);
            }
            return languages;
        }


     
    }
}

