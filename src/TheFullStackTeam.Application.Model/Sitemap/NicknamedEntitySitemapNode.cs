using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Application.Model.Sitemap
{
    public class NicknamedEntitySitemapNode : SitemapNodeBase

    {
        public static Expression<Func<NicknamedEntity, NicknamedEntitySitemapNode>> Projection
        {
            get
            {
                return x => new NicknamedEntitySitemapNode()
                {
                    Url = x.Moniker,
                    LastModificationDate = x.Modified ?? x.Created
                };
            }
        }
    }
}
