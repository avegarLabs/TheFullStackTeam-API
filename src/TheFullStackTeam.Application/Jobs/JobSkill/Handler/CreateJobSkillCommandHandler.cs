using MediatR;
using TheFullStackTeam.Application.Jobs.JobSkill.Command;
using TheFullStackTeam.Application.Jobs.JobSkill.Results;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.JobSkill.Handler
{
    public class CreateJobSkillCommandHandler : AppRequestHandler, IRequestHandler<CreateJobSkillCommand, JobSkillsCommandsResults>
    {
        private readonly IMonikerService _monikerService;
        public CreateJobSkillCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _monikerService = moniker;
        }

        public async Task<JobSkillsCommandsResults> Handle(CreateJobSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = _context.Skills.Where(s => s.Moniker.Equals(request.Model.SkillMoniker)).SingleOrDefault();

            Domain.Entities.JobSkill jobSkill = request.Model;
            jobSkill.JobId= request.JobId;
            if (skill == null)
            {
                skill = new Skill()
                {
                    Moniker = await _monikerService.FindValidMoniker<Skill>(request.Model.SkillName),
                    Name = request.Model.SkillName,
                    JobSkills = new List<Domain.Entities.JobSkill> { jobSkill }

                };
                await _context.Skills.AddAsync(skill, cancellationToken);
            }
            else
            {
                skill.JobSkills.Add(jobSkill);
                _context.Skills.Update(skill);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return new JobSkillsCommandsResults(jobSkill);
        }
    }
}
