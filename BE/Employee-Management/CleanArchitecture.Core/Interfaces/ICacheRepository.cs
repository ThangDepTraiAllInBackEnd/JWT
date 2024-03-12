using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
	public interface ICacheRepository

	{
		/// <summary>
		/// Get cache data by cache key
		/// </summary>
		/// <typeparam name="T">reuturn type</typeparam>
		/// <param name="key">key to get data</param>
		/// <returns>Data in cache with type T</returns>
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2024/2/1
		T GetCache<T>(string key);

		/// <summary>
		/// Creaet a cache data 
		/// </summary>
		/// <param name="key">Key to get data </param>
		/// <param name="value">Value to save into cache</param>
		/// <param name="expirationTime">Expiry time for cache key</param>
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2024/2/1
		void SetCache<T>(string key, T value, TimeSpan expirationTime);

		/// <summary>
		/// update cache data by cache key
		/// </summary>
		/// <param name="key">cache key to update</param>
		/// <param name="value">new value to update</param>
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2024/2/1
		void UpdateCache<T>(string key, T value);

		/// <summary>
		/// check is cache exsit or not
		/// </summary>
		/// <param name="key">key to check </param>
		/// <returns>True - cache exist|| False - cache do not exist</returns>
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2024/2/1
		bool IsCacheExist(string key);
	}
}
