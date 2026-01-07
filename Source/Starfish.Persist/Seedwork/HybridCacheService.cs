using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Caching;

namespace Nerosoft.Starfish.Persist;

/// <summary>
/// Hybrid cache service that selects the default caching provider based on configuration.
/// </summary>
internal class HybridCacheService : ICacheService
{
	private readonly ICacheService _cacheService;

	public HybridCacheService(IServiceProvider provider, IConfiguration configuration)
	{
		var defaultCacheProvider = configuration.GetValue<string>("Euonia:Caching:DefaultProvider") ?? "Memory";
		_cacheService = provider.GetKeyedService<ICacheService>(defaultCacheProvider);
	}

	public TValue Get<TValue>(string key)
	{
		return _cacheService.Get<TValue>(key);
	}

	public bool TryGet<TValue>(string key, out TValue value)
	{
		return _cacheService.TryGet(key, out value);
	}

	public TValue GetOrAdd<TValue>(string key, Func<TValue> factory, TimeSpan? timeout = null)
	{
		return _cacheService.GetOrAdd(key, factory, timeout);
	}

	public TValue GetOrAdd<TValue>(string key, Func<TValue> factory, DateTime timeout, bool isUtcTime = true)
	{
		return _cacheService.GetOrAdd(key, factory, timeout, isUtcTime);
	}

	public TValue AddOrUpdate<TValue>(string key, Func<TValue> factory, TimeSpan? timeout = null)
	{
		return _cacheService.AddOrUpdate(key, factory, timeout);
	}

	public TValue AddOrUpdate<TValue>(string key, Func<TValue> factory, DateTime timeout, bool isUtcTime = true)
	{
		return _cacheService.AddOrUpdate(key, factory, timeout, isUtcTime);
	}

	public TValue AddOrUpdate<TValue>(string key, TValue value, TimeSpan? timeout = null)
	{
		return _cacheService.AddOrUpdate(key, value, timeout);
	}

	public TValue AddOrUpdate<TValue>(string key, TValue value, DateTime timeout, bool isUtcTime = true)
	{
		return _cacheService.AddOrUpdate(key, value, timeout, isUtcTime);
	}

	public TValue AddOrUpdate<TValue>(CacheItem<TValue> item)
	{
		return _cacheService.AddOrUpdate(item);
	}

	public bool Remove<TValue>(string key)
	{
		return _cacheService.Remove<TValue>(key);
	}

	public Task<Tuple<bool, TValue>> TryGetAsync<TValue>(string key, CancellationToken cancellationToken = default)
	{
		return _cacheService.TryGetAsync<TValue>(key, cancellationToken);
	}

	public Task<TValue> GetOrAddAsync<TValue>(string key, Func<Task<TValue>> factory, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
	{
		return _cacheService.GetOrAddAsync(key, factory, timeout, cancellationToken);
	}

	public Task<TValue> GetOrAddAsync<TValue>(string key, Func<Task<TValue>> factory, DateTime timeout, bool isUtcTime = true, CancellationToken cancellationToken = default)
	{
		return _cacheService.GetOrAddAsync(key, factory, timeout, isUtcTime, cancellationToken);
	}

	public Task<TValue> AddOrUpdateAsync<TValue>(string key, Func<Task<TValue>> factory, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
	{
		return _cacheService.AddOrUpdateAsync(key, factory, timeout, cancellationToken);
	}

	public Task<TValue> AddOrUpdateAsync<TValue>(string key, Func<Task<TValue>> factory, DateTime timeout, bool isUtcTime = true, CancellationToken cancellationToken = default)
	{
		return _cacheService.AddOrUpdateAsync(key, factory, timeout, isUtcTime, cancellationToken);
	}

	public Task<TValue> AddOrUpdateAsync<TValue>(Func<Task<CacheItem<TValue>>> factory, CancellationToken cancellationToken = default)
	{
		return _cacheService.AddOrUpdateAsync(factory, cancellationToken);
	}
}