using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

// Base class for API controllers
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
                                      ?? throw new InvalidOperationException("Mediator service not found.");
                             
    protected ActionResult HandleResult<T>(T result)
    {
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
