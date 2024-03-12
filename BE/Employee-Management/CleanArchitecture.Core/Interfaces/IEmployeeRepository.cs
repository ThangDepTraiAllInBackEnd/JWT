using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
	public interface IEmployeeRepository : IBaseRepository<Employee>
	{
		/// <summary>
		/// Check is Employee is exist or not
		/// </summary>
		/// <param name="code"> Employee Code</param>
		/// <returns>
		/// true- Employee Code exist
		/// false- Employee Code doesn't exist
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/8
		Task<bool> IsEmployeeCodeExistAsync(string code);

		/// <summary>
		/// Check is Employee phone is exist or not
		/// </summary>
		/// <param name="phone"> Employee phone number </param>
		/// <returns>
		/// true- Employee phone exist
		/// false- Employee phone doesn't exist
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		bool IsEmployeePhoneExist(string phone);

		/// <summary>
		/// Check is Employee email is exist or not
		/// </summary>
		/// <param name="email"> Employee email </param>
		/// <returns>
		/// true- Employee email exist
		/// false- Employee email doesn't exist
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		bool IsEmployeeEmailExist(string email);

		/// <summary>
		/// Get biggest employeeCode
		/// </summary>
		/// <returns>
		/// biggest employee Code
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/21
		Task<string> GetBiggestEmployeeCodeAsync();

		/// <summary>
		///  Get number of page base table and page size
		/// </summary>
		/// <returns>Total employee record in DB</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2023/12/2
		public Task<int> CountTotalRecord<Employee>();
	}
}
