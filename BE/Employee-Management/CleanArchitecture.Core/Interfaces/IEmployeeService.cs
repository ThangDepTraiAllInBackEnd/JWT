using CleanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
	public interface IEmployeeService : IBaseService<Employee>
	{

		/// <summary>
		/// recevie a file and save to db
		/// </summary>
		/// <param name="excelFile">file to save to db</param>
		/// <param name="isImport">User want to import empoyee or just want to preview file</param>
		/// <returns>S( sucsess with data or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public Task<ServiceResult> PreviewFileAsync(IFormFile excelFile);

		/// <summary>
		/// exprot file for user base user's current page, page size and search key word
		/// </summary>
		/// <param name="page">Current page </param>
		///  <param name="pageSize">record'number /page </param>
		///  <param name="key">search key </param>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/15/1
		public Task<ServiceResult> ExportFileAsync(int page, int pageSize, string key);

		/// <summary>
		/// Convert data from DB to service format
		/// </summary>
		/// <param name="id">id to find by</param>
		/// <returns>Serivce result ( sucsess with data or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		Task<ServiceResult> GetByIdServiceAsync(Guid id);

		/// <summary>
		/// Calculate to get bigger employeeCode
		/// </summary>
		/// <returns>Serivce result ( sucsess with data or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/21
		Task<ServiceResult> CalEmployeeCodeAsync();

		/// <summary>
		/// Action before update entity into database
		/// </summary>
		/// <param name="employee">Entity to manipulate</param>
		/// <returns>
		/// true- entity valid to update
		/// false-  entity valid not to update
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public Task<bool> BeforeUpdateAsync(Employee employee);

		/// <summary>
		/// Delete employess by EmployeeId list
		/// </summary>
		/// <param name="employees">Employee list to delete</param>
		/// <returns>Serivce result ( sucsess with data or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public Task<ServiceResult> DeleteManyAsync(List<Employee> employees);

		/// <summary>
		/// Get total employee record in DB
		/// </summary>
		/// <returns>Serivce result ( sucsess with data or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public Task<ServiceResult> GetTotalRecordAsync();

		/// <summary>
		/// insert all valid employee base cache key from previous preview
		/// </summary>
		///  <param name="key">Cache key to get data to insert employee into db </param>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/2/1
		public Task<ServiceResult> ImportFileAsync(string key);
	}
}
