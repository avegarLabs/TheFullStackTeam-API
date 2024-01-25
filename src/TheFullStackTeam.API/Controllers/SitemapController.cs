using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFullStackTeam.Application.Sitemap;

namespace TheFullStackTeam.API.Controllers
{
    /// <summary>
    /// Sitemap Controller
    /// </summary>
    /// <seealso cref="BaseController{SitemapController}" />
    /// <seealso cref="Controller" />
    [Produces("application/json")]
    [Route("[controller]")]
    public class SitemapController : BaseController<SitemapController>
    {
        /// <summary>
        /// Gets Sitemap
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return await Mediator.Send(new GetSitemap());
        }
    }
}
