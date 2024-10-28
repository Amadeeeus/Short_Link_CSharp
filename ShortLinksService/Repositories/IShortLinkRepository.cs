using ShortLinksService.Entities;
using ShortLinksService.Models.MongoEntities;

namespace ShortLinksService.Repositories;

public interface IShortLinkRepository
{
    Task<string> GetOriginalAsync(string url, CancellationToken token);
    Task DeleteAsync(string url, CancellationToken token);
    Task<string> CreateAsync(MongoEntity entity, CancellationToken token);
}