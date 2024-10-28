using MediatR;

namespace ShortLinksService.Queries.Get;

public class GetOriginalLinkRequest : IRequest<string>
{
    public string shortUrl { get; set; }
}
