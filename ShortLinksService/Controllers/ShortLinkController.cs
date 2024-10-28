using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShortLinksService.Commands.Create;
using ShortLinksService.Commands.Delete;
using ShortLinksService.Entities;
using ShortLinksService.Queries.Get;

namespace ShortLinksService.Controllers;

[ApiController]
[Route("/route/")]
public class ShortLinkController: ControllerBase
{
    //private readonly IValidator<ShortLinkController> _validator;
    private readonly ILogger<ShortLinkController> _logger;
    private readonly IMediator _mediator;
 
    public ShortLinkController(ILogger<ShortLinkController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
       // _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> RedirectLink([FromQuery] string link)
    {
        if (string.IsNullOrWhiteSpace(link))
        {
            throw new KeyNotFoundException();
        }

        _logger.LogInformation("Redirecting from short link {link}", link);
        var result = await _mediator.Send(new GetOriginalLinkRequest{shortUrl = link});
        _logger.LogInformation("TO {result}", result);
        return Redirect(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShortLink([FromQuery]string routeUrl, string password)
    {
        _logger.LogInformation("Creating short link from {routeUrl}", routeUrl);
        var result = await _mediator.Send(new CreateShortLinkRequest()
        {
            Url = routeUrl, 
            Password = password
        });
        _logger.LogInformation("returned short link: {result}", result);
        return Ok("ShortLink - "+result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteShortLink([FromQuery]string routeUrl, string password)
    {
        _logger.LogInformation("Deleting short link from {routeUrl}", routeUrl);
        await _mediator.Send(new DeleteShortLinkRequest{ShortLink = routeUrl, Password = password});
        return Ok();
    }
}