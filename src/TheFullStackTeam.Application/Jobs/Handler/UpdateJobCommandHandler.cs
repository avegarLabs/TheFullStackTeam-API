using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Jobs.Command;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.Handler
{
    public class UpdateJobCommandHandler : AppRequestHandler, IRequestHandler<UpdateJobCommand, CreateJobCommandResult>
    {
        private readonly IMonikerService _moniker;
        public UpdateJobCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _moniker = moniker;
        }

        public async Task<CreateJobCommandResult> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs.Where(j => j.Id == request.JobId).SingleOrDefaultAsync(cancellationToken);
            var country = await _context.Countries.Where(c => c.Id.Equals(request.JobPost.CountryId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (job == null)
            {
                throw new NotFoundException(nameof(Jobs), request.JobId);
            }
            await ClearJobsRelations(job, cancellationToken);

            job.JobTitle = request.JobPost.JobTitle;
            job.JobDescription = request.JobPost.JobDescription;
            job.CountryId = country.Id;
            job.JobsJobTypes = await UpdateJobsType(job, request.JobPost.JobsJobTypes);
            job.JobContractTypes = await UpdateJobContractType(job, request.JobPost.JobContractTypes);
            await UpdateSalaryInJobs(job, request.JobPost.JobsSalaryTypes);

            if (request.JobPost.LanguagesRequired.Count() > 0)
            {
                job.RequiredLanguages.Clear();
                job.RequiredLanguages = GetLanguegeList(job, request.JobPost.LanguagesRequired);
            }
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync(cancellationToken);
            return new CreateJobCommandResult(job);

        }

        private async Task<Task> ClearJobsRelations(Domain.Entities.Job job, CancellationToken cancellationToken)
        {
            var typeInJobs = await _context.JobsJobTypes.Where(js => js.JobId == job.Id).ToListAsync(cancellationToken);
            _context.JobsJobTypes.RemoveRange(typeInJobs);
            var jobContractType = await _context.JobContractTypes.Where(jc => jc.JobId == job.Id).ToListAsync(cancellationToken);
            _context.JobContractTypes.RemoveRange(jobContractType);
            return Task.CompletedTask;
        }

        private async Task<ICollection<JobContractType>> UpdateJobContractType(Domain.Entities.Job job, List<JobContractTypeModel> jobContractTypes)
        {
            List<JobContractType> list = new List<JobContractType>();
            foreach(var item in jobContractTypes)
            {
                var contract = new JobContractType()
                {
                    ContractTypeName = item.ContractTypeName
                };
                list.Add(contract);
            }
            return list;
        }

        private async Task<ICollection<JobsJobType>> UpdateJobsType(Domain.Entities.Job job, List<JobJobTypeModel> jobsJobTypes)
        {
            List<JobsJobType> list = new List<JobsJobType>();
            foreach(var item in jobsJobTypes)
            {
                var types = new JobsJobType()
                {
                    JobTypeName = item.JobTypeName
                };
                list.Add(types);
            }
            return list;
        }

        private async Task UpdateSalaryInJobs(Domain.Entities.Job job, JobSalaryTypeModel jobsSalaryTypes)
        {
            var jobSalary = _context.jobsSalaryTypes.Where(js => js.JobId.Equals(job.Id)).SingleOrDefault();
            if(jobSalary != null)
            {
                jobSalary.Currency = jobsSalaryTypes.Currency;
                jobSalary.MaxAmount = jobsSalaryTypes.MaxAmount;
                jobSalary.MinAmount = jobsSalaryTypes.MinAmount;
                jobSalary.SalaryTypeName = jobsSalaryTypes.SalaryTypeName;
                _context.jobsSalaryTypes.Update(jobSalary);
            }
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
