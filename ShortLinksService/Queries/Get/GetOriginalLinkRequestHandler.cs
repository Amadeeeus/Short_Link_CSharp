using MediatR;
using MongoDB.Driver;
using ShortLinksService.Queries.Get;
using ShortLinksService.Repositories;

namespace ShortLinksService.Controllers;

public class GetOriginalLinkRequestHandler: IRequestHandler<GetOriginalLinkRequest, string>
{
    private readonly IShortLinkRepository _repository;
    public GetOriginalLinkRequestHandler(IShortLinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(GetOriginalLinkRequest request, CancellationToken token)
    {
        return await _repository.GetOriginalAsync(request.shortUrl, token);
    }
}