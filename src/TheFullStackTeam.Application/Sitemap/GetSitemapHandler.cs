using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMvcSitemap;
using TheFullStackTeam.Application.Model.Sitemap;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Sitemap
{
    public class GetSitemapHandler : IRequestHandler<GetSitemap, IActionResult>
    {
        private readonly TheFullStackTeamDbContext _context;

        public GetSitemapHandler(TheFullStackTeamDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Handle(GetSitemap request, CancellationToken cancellationToken)
        {
            var suportedLanguages = _context.Languages.Select(l => l.IsoCode);

            var professionals = await _context.Professionals
                .Where(a => a.IsDeleted == false)
                .Select(NicknamedEntitySitemapNode.Projection).ToListAsync(cancellationToken: cancellationToken);

            var organizations = await _context.Organizations
               .Where(a => a.IsDeleted == false)
               .Select(NicknamedEntitySitemapNode.Projection).ToListAsync(cancellationToken: cancellationToken);

            var jobs = await _context.Jobs
                 .Where(a => a.IsDeleted == false)
                 .Select(NicknamedEntitySitemapNode.Projection).ToListAsync(cancellationToken: cancellationToken);


            List<SitemapNode> nodes = new();

            foreach (var lang in suportedLanguages)
            {
                foreach (var pro in professionals)
                {
                    nodes.Add(new SitemapNode($"{SitemapConstants.BASE_URL}/{lang}/{SitemapConstants.PROFESSIONALS_PATH}/{pro.Url}")
                    {
                        ChangeFrequency = ChangeFrequency.Monthly,
                        LastModificationDate = new DateTime(
                                pro.LastModificationDate.Year,
                                pro.LastModificationDate.Month,
                                pro.LastModificationDate.Day,
                                pro.LastModificationDate.Hour,
                                pro.LastModificationDate.Minute,
                                pro.LastModificationDate.Second,
                                DateTimeKind.Local),
                        Priority = 0.8M
                    });
                }

                foreach (var org in organizations)
                {
                    nodes.Add(new SitemapNode($"{SitemapConstants.BASE_URL}/{lang}/{SitemapConstants.ORGANIZATIONS_PATH}/{org.Url}")
                    {
                        ChangeFrequency = ChangeFrequency.Weekly,
                        LastModificationDate = new DateTime(
                            org.LastModificationDate.Year,
                            org.LastModificationDate.Month,
                            org.LastModificationDate.Day,
                            org.LastModificationDate.Hour,
                            org.LastModificationDate.Minute,
                            org.LastModificationDate.Second,
                            DateTimeKind.Local),
                        Priority = 0.8M
                    });
                }

                foreach (var job in jobs)
                {
                    nodes.Add(new SitemapNode($"{SitemapConstants.BASE_URL}/{lang}/{SitemapConstants.JOBS_PATH}/{job.Url}")
                    {
                        ChangeFrequency = ChangeFrequency.Weekly,
                        LastModificationDate = new DateTime(
                            job.LastModificationDate.Year,
                            job.LastModificationDate.Month,
                            job.LastModificationDate.Day,
                            job.LastModificationDate.Hour,
                            job.LastModificationDate.Minute,
                            job.LastModificationDate.Second,
                            DateTimeKind.Local),
                        Priority = 0.8M
                    });
                }
            }
            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}


