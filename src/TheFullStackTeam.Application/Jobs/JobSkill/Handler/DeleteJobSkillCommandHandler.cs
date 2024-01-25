using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Jobs.JobSkill.Command;
using TheFullStackTeam.Application.Jobs.JobSkill.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.JobSkill.Handler
{
    internal class DeleteJobSkillCommandHandler : AppRequestHandler, IRequestHandler<DeleteJobSkillCommand, DeleteJobSkillCommandResult>
    {
        public DeleteJobSkillCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<DeleteJobSkillCommandResult> Handle(DeleteJobSkillCommand request, CancellationToken cancellationToken)
        {
            var jsk = await _context.JobSkill.Where(item => item.Id == request.SkillId).SingleOrDefaultAsync(cancellationToken);
            if (jsk != null)
            {
                Console.WriteLine(jsk.SkillName);
                _context.JobSkill.Remove(jsk);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteJobSkillCommandResult(true);

        }
    }
}
