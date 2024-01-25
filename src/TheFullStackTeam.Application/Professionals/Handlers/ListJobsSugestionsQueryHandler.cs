using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Queries;
using TheFullStackTeam.Application.Professionals.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers
{
    public class ListJobsSugestionsQueryHandler : AppRequestHandler, IRequestHandler<ListJobsSugestionsQuery, ListJobSugestionQueryResults>
    {
        public ListJobsSugestionsQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ListJobSugestionQueryResults> Handle(ListJobsSugestionsQuery request, CancellationToken cancellationToken)
        {
            var professionalSkills = await _context.ProfessionalSkills.Where(p => p.ProfessionalId.Equals(request.professionalId)).Select(ps => ps.Skill.Moniker).ToListAsync(cancellationToken);
            List<JobSugestionsListItem> resultList = new List<JobSugestionsListItem>();

            if (professionalSkills == null)
            {
                throw new Exception($"Not found skill in professional ID: {request.professionalId}");
            }

            var jobsList = await _context.Jobs.Where(j => j.Active.Equals("1")).ToListAsync(cancellationToken);
            foreach (var job in jobsList)
            {
                var listMath =  SkillsMatch(professionalSkills, job);
                if (listMath.Count() > 0)
                {
                    var jobSub = new JobSugestionsListItem()
                    {
                        JobTitle = job.JobTitle,
                        DegreeTrust = CalculatePercent(listMath.Count(), job.JobSkills.Count())
                    };
                    resultList.Add(jobSub);
                }
            }
            return new ListJobSugestionQueryResults(resultList);
        }

        private double CalculatePercent(double v1, double v2)
        {
            double calc = v1 / v2;
            return Math.Round(calc, 2) ;
        }

        private List<string> SkillsMatch(List<string> professionalSkills, Domain.Entities.Job job)
        {
            var skillInJob = _context.JobSkill.Where(jk => jk.JobId == job.Id).Select(js => js.Skill.Moniker).ToList();
            var coincidence = professionalSkills.Intersect(skillInJob).ToList();
            return coincidence;
        }
    }
}
