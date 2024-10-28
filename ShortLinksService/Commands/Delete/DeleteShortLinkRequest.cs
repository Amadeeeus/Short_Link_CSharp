using System.Windows.Input;
using MediatR;
using ShortLinksService.Controllers;

namespace ShortLinksService.Commands.Delete;

public class DeleteShortLinkRequest : IRequest
{
    public string ShortLink { get; set; }
    public string Password { get; set; }
}
