using CleanArchitecture.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
	public class CacheRepository : ICacheRepository
	{

		private IMemoryCache _memoryCache;

		public CacheRepository(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		/// <summary>
		/// Get cache data by cache key
		/// </summary>
		/// <typeparam name="T">reuturn type</typeparam>
		/// <param name="key">key to get data</param>
		/// <returns>Data in cache with type T</returns>
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2024/2/1
		public T GetCache<T>(string key)
		{
			return _memoryCache.Get<T>(key);
		}

		/// <summary>
		/// Creaet a cache data 
		/// </summary>
		/// <param name="key">Key to get data </param>
		/// <param name="value">Value to save into cache</param>
		/// <param name="expirationTime">Expiry time for cache key</param>
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2024/2/1
		public void SetCache<T>(string key, T value, TimeSpan expirationTime)
		{
			_memoryCache.Set(key, value, expirationTime);
		}

		/// <summary>
		/// update cache data by cache key
		/// </summary>
		/// <param name="key">cache key to update</param>
		/// <param name="value">new value to update</param>
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2024/2/1
		public void UpdateCache<T>(string key, T value)
		{
			if (IsCacheExist(key))
			{
				_memoryCache.Remove(key);
			}
			_memoryCache.Set(key, value);
		}

		/// <summary>
		/// check is cache exsit or not
		/// </summary>
		/// <param name="key">key to check </param>
		/// <returns>True - cache exist|| False - cache do not exist</returns>
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2024/2/1
		public bool IsCacheExist(string key)
		{
			return _memoryCache.TryGetValue(key, out _);
		}
	}
}
