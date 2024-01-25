using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Jobs.Command;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.Handler
{
    public class DeleteJobsCommandHandler : AppRequestHandler, IRequestHandler<DeleteJobCommand, DeleteJobCommandResult>
    {
        public DeleteJobsCommandHandler(TheFullStackTeamDbContext context) : base(context) { }
        public async Task<DeleteJobCommandResult> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs.Where(j => j.Id == request.JobId).SingleOrDefaultAsync(cancellationToken);
            if (job != null)
            {
                job.RequiredLanguages.Clear();
                var skills = await _context.JobSkill.Where(sk => sk.JobId == job.Id).AsNoTracking().ToListAsync(cancellationToken);
                _context.JobSkill.RemoveRange(skills);
                var responsabilities = await _context.JobResponsabilities.Where(sk => sk.JobId == job.Id).AsNoTracking().ToListAsync(cancellationToken);
                _context.JobResponsabilities.RemoveRange(responsabilities);
                var salary = await _context.jobsSalaryTypes.Where(sk => sk.JobId == job.Id).AsNoTracking().ToListAsync(cancellationToken);
                _context.jobsSalaryTypes.RemoveRange(salary);
                var jobTypes = await _context.JobsJobTypes.Where(sk => sk.JobId == job.Id).AsNoTracking().ToListAsync(cancellationToken);
                _context.JobsJobTypes.RemoveRange(jobTypes);
                var contracts = await _context.JobContractTypes.Where(sk => sk.JobId == job.Id).AsNoTracking().ToListAsync(cancellationToken);
                _context.JobContractTypes.RemoveRange(contracts);
                _context.Jobs.Remove(job);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteJobCommandResult(true);
        }
    }
}
