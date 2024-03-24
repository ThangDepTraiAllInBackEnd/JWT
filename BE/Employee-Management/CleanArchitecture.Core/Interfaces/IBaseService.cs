using CleanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
	public interface IBaseService<T> where T : class
	{
		/// <summary>
		/// get all data and validate before return data for user
		/// </summary>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<ServiceResult> GetAllServiceAsync();
		/// <summary>
		/// Check is all entity' properies is valid -> insert
		/// </summary>
		/// <param name="entity">Entity to check </param>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<ServiceResult> InsertServiceAsync(T entity);
		/// <summary>
		/// Check is all entity' properies is valid -> update
		/// </summary>
		/// <param name="entity">Entity to check </param>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		Task<ServiceResult> UpdateServiceAsync(T entity);
        /// <summary>
        /// Check is all entity' properies is valid -> delete
        /// </summary>
        /// <param name="entity">Entity to check </param>
        /// <returns>Service result ( sucsess or failed with all details )</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        Task<ServiceResult> DeleteServiceAsync(T entity);
        /// <summary>
        /// Paging records base page and pageSize
        /// </summary>
        /// <param name="page">Current page </param>
        ///  <param name="pageSize">record'number /page </param>
        ///  <param name="key">search key </param>
        /// <returns>Service result ( sucsess or failed with all details )</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/15/1
        Task<ServiceResult> PagingServiceAsync(int page, int pageSize, string key);
	}
}
