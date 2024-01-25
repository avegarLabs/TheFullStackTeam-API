using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Jobs.JobSkill.Command;
using TheFullStackTeam.Application.Jobs.JobSkill.Results;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.JobSkill.Handler
{
    public class UpdateJobSkillCommandHandler : AppRequestHandler, IRequestHandler<UpdateJobSkillCommand, JobSkillsCommandsResults>
    {

        private readonly IMonikerService _monikerService;
        public UpdateJobSkillCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _monikerService = moniker;
        }

        public async Task<JobSkillsCommandsResults> Handle(UpdateJobSkillCommand request, CancellationToken cancellationToken)
        {
            var jobSkill = await _context.JobSkill.Where(js => js.JobId.Equals(request.JobId) && js.Id.Equals(request.JobSkillId)).SingleOrDefaultAsync(cancellationToken);
            var skill = await _context.Skills.Where(s => s.Moniker.Equals(request.Model.SkillMoniker)).SingleOrDefaultAsync(cancellationToken);
            if (skill == null)
            {
                skill = new Skill()
                {
                Moniker = await _monikerService.FindValidMoniker<Skill>(request.Model.SkillName),
                Name = request.Model.SkillName               
                };
                await _context.Skills.AddAsync(skill, cancellationToken);

                jobSkill.SkillName = request.Model.SkillName;
            }
            else
            {
                jobSkill.SkillName = skill.Name;
            }
            _context.JobSkill.Update(jobSkill);
            await _context.SaveChangesAsync(cancellationToken);
            return new JobSkillsCommandsResults(jobSkill);
        }
    }
}
