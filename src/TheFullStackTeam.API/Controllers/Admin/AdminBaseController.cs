using Microsoft.AspNetCore.Mvc;

namespace TheFullStackTeam.API.Controllers.Admin
{
    /// <summary>
    /// Admin Base Controller Class for TFST API
    /// </summary>
    /// <seealso cref="BaseController{AdminBaseController}" />
    [Area("Admin")]
    public abstract class AdminBaseController: BaseController<AdminBaseController>
    {
    }
}
