using System.Windows.Input;
using MediatR;
using Visus.Cuid;

namespace ShortLinksService.Commands.Create;

public class CreateShortLinkRequest : IRequest<string>
{
    public string Url { get; set; }
    public string Password { get; set; }
    public string ShortUrl { get; set; }
}
