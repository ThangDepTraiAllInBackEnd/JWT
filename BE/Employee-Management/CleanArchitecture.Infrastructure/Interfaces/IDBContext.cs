using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Interfaces
{
	public interface IDBContext
	{
		IDbConnection Connection { get; }
		IDbTransaction Transaction { get; set; }

		/// <summary>
		/// Get all record in a table in Database 
		/// </summary>
		/// <returns>An Entity List with type T or null if not found</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<List<T>> GetAllAsync<T>();
		/// <summary>
		/// Find entity by id
		/// </summary>
		/// <param name="id">Entity's id to find </param>
		/// <returns>An Entity with type T or null if not found</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<T> GetByIdAsync<T>(Guid? id);
		/// <summary>
		/// uinsert new entity to db
		/// </summary>
		/// <param name="entity">Entity to insert </param>
		/// <returns>nummber of record affected</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<int> InsertAsync<T>(T entity);
		/// <summary>
		///  Update entity by Id
		/// </summary>
		/// <param name="entity">Entity to Update </param>
		/// <returns>nummber of record affected</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<int> UpdateAsync<T>(T entity);
        /// <summary>
        ///  Delete entity by Id
        /// </summary>
        /// <param name="entity">Entity to Delete </param>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        Task<int> DeleteAsync<T>(T entity);
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
		Task<List<T>> PagingAsync<T>(int offSet, int next, string key);
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
