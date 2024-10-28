using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShortLinksService.Entities;
using ShortLinksService.Models.MongoEntities;
using Sprache;

namespace ShortLinksService.Repositories;

public class ShortLinkRepository : IShortLinkRepository
{
    private readonly IDistributedCache _distributedCache;
    private readonly IMongoCollection<MongoEntity> _mongoCollection; 
    public ShortLinkRepository(IDistributedCache distributedCache, IOptions<MongoEntitySettings> mongoEntitySettings)
    {
        var mongoClient = new MongoClient(mongoEntitySettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoEntitySettings.Value.DatabaseName);
        _mongoCollection = mongoDatabase.GetCollection<MongoEntity>(mongoEntitySettings.Value.CollectionName);
        _distributedCache = distributedCache;
    }

    public async Task<string> GetOriginalAsync(string url, CancellationToken token)
    {
        var cached =  await _distributedCache.GetStringAsync("shortlinks:"+url, token);
        if (!string.IsNullOrWhiteSpace(cached))
        {
            return cached;
        }
        var entity = _mongoCollection.FindAsync(x=>x.ShortUrl == url).Result.FirstOrDefault();
        var cacheOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));
        await _distributedCache.SetStringAsync(entity.ShortUrl, entity.Url,cacheOptions, token);
        return entity.Url;
    }

    public async Task DeleteAsync(string url, CancellationToken token)
    {
        await _distributedCache.RemoveAsync(url, token);
        await _mongoCollection.DeleteOneAsync(x => x.ShortUrl == url, token);
    }

    public async Task<string> CreateAsync(MongoEntity entity, CancellationToken token)
    {
        await _mongoCollection.InsertOneAsync(entity, null, token);
        var cacheOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));
        await _distributedCache.SetStringAsync(entity.ShortUrl, entity.Url, cacheOptions, token);
        return entity.ShortUrl;
    }

    
}