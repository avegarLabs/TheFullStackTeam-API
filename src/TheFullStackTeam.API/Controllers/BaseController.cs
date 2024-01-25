using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFullStackTeam.API.Controllers;

/// <summary>
/// Controller base
/// </summary>
/// <typeparam name="TController"></typeparam>
[ApiController]
[Authorize]
[Route("[controller]")]
public class BaseController<TController> : Controller where TController : BaseController<TController>
{
    private IMediator _mediator = null!;
    private ILogger<TController> _logger = null!;

    /// <summary>
    /// Application logger
    /// </summary>
    protected ILogger<TController> Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<TController>>()!)!;

    /// <summary>
    /// Mediator implementation
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}