using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		/// <summary>
		/// Get all record in a table in Database 
		/// </summary>
		/// <returns>An Entity List with type T or null if not found</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<List<T>> GetAllAsync();
		/// <summary>
		/// Find entity by id
		/// </summary>
		/// <param name="id">Entity's id to find </param>
		/// <returns>An Entity with type T or null if not found</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<T> GetByIdAsync(Guid? id);
		/// <summary>
		/// Get all record in a table in Database 
		/// </summary>
		/// <param name="entity">Entity to insert </param>
		/// <returns>nummber of record affected</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<int> InsertAsync(T entity);
		/// <summary>
		///  Update entity by Id
		/// </summary>
		/// <param name="entity">Entity to Update </param>
		/// <returns>nummber of record affected</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<int> UpdateAsync(T entity);
        /// <summary>
        ///  Delete entity by Id
        /// </summary>
        /// <param name="entity">Entity to Delete </param>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        Task<int> DeleteAsync(T entity);
        /// <summary>
        ///  Delete all record in an table
        /// </summary>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        Task<int> DeleteAllAsync();
		/// <summary>
		///  Get recoreds to paging by offset and next
		/// </summary>
		/// <param name="offSet">Record start </param>
		/// <param name="next">Number of record have to get </param>
		///  <param name="key">key to search </param>
		/// <returns>List T was paging</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<List<T>> PagingAsync(int offSet, int next, string key);
		/// <summary>
		///  Get number of page base table and page size
		/// </summary>
		/// <param name="pageSize">Record start </param>
		/// <param name="key">key to search </param>
		/// <returns>number of page base table and page size</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<int> GetPageSizeAsync<T>(int pageSize, string key);
	}

}
