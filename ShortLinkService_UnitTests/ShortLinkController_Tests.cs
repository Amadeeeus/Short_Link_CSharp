
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MediatR;
using ShortLinksService.Controllers;
using ShortLinksService.Commands;
using ShortLinksService.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ShortLinksService.Commands.Create;
using ShortLinksService.Commands.Delete;
using ShortLinksService.Queries.Get;

public class ShortLinkControllerTests
{
    private readonly Mock<ILogger<ShortLinkController>> _loggerMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ShortLinkController _controller;

    public ShortLinkControllerTests()
    {
        _loggerMock = new Mock<ILogger<ShortLinkController>>();
        _mediatorMock = new Mock<IMediator>();
        _controller = new ShortLinkController(_loggerMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task RedirectLink_ShouldRedirectToOriginalLink()
    {
        var shortLink = "test-link";
        var originalLink = "https://example.com";
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetOriginalLinkRequest>(), default))
            .ReturnsAsync(originalLink);
        var result = await _controller.RedirectLink(shortLink) as RedirectResult;
        Assert.NotNull(result);
        Assert.Equal(originalLink, result.Url);
    }

    [Fact]
    public async Task CreateShortLink_ShouldReturnShortLink()
    {
        // Arrange
        var routeUrl = "https://example.com";
        var password = "secure";
        var shortLink = "short-link";
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateShortLinkRequest>(), default))
            .ReturnsAsync(shortLink);
        var result = await _controller.CreateShortLink(routeUrl, password) as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal($"ShortLink - {shortLink}", result.Value);
    }

    [Fact]
    public async Task DeleteShortLink_ShouldReturnOk()
    {
        var routeUrl = "short-link";
        var password = "secure";
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteShortLinkRequest>(), default));
        var result = await _controller.DeleteShortLink(routeUrl, password);
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task RedirectLink_WithEmptyLink_ShouldThrowKeyNotFoundException()
    {
        var shortLink = "";
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _controller.RedirectLink(shortLink));
    }
}

