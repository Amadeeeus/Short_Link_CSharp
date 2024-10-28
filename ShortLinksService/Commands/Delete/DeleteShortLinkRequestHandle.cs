using MediatR;
using ShortLinksService.Repositories;

namespace ShortLinksService.Commands.Delete;

public class DeleteShortLinkRequestHandle:IRequestHandler<DeleteShortLinkRequest>
{
    private readonly IShortLinkRepository _shortLinkRepository;

    public DeleteShortLinkRequestHandle(IShortLinkRepository shortLinkRepository)
    {
        _shortLinkRepository = shortLinkRepository;
    }

    public async Task Handle(DeleteShortLinkRequest request, CancellationToken cancellationToken)
    {
        await _shortLinkRepository.DeleteAsync(request.ShortLink, cancellationToken);
    }
}