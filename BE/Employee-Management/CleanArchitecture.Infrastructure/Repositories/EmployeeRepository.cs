using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CleanArchitecture.Infrastructure.Repositories
{
	public class EmployeeRepository : DBContext<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(IDBContext dbContext) : base(dbContext)
		{
		}

		/// <summary>
		/// Insert an employee
		/// </summary>
		/// <param name="employee">employee to insert </param>
		/// <returns>Number of record affect</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2023/12/2
		public async override Task<int> InsertAsync(Employee employee)
		{
			var parameters = new DynamicParameters();
			parameters.Add($"@EmployeeId", employee.EmployeeId);
			parameters.Add($"@EmployeeCode", employee.EmployeeCode);
			parameters.Add($"@EmployeeName", employee.EmployeeName);
			parameters.Add($"@BirthDate", employee.BirthDate);
			parameters.Add($"@Gender", employee.Gender);
			parameters.Add($"@Identity", employee.Identity);
			parameters.Add($"@ProvideDate", employee.ProvideDate);
			parameters.Add($"@ProvidePlace", employee.ProvidePlace);
			parameters.Add($"@Address", employee.Address);
			parameters.Add($"@MobilePhone", employee.MobilePhone);
			parameters.Add($"@LandLine", employee.LandLine);
			parameters.Add($"@Email", employee.Email);
			parameters.Add($"@BankAccount", employee.BankAccount);
			parameters.Add($"@BankName", employee.BankName);
			parameters.Add($"@Branch", employee.Branch);
			parameters.Add($"@CreatedDate", employee.CreatedDate);
			parameters.Add($"@CreatedBy", employee.CreatedBy);
			parameters.Add($"@ModifiedDate", employee.ModifiedDate);
			parameters.Add($"@PositionName", employee.PositionName);
			parameters.Add($"@ModifiedBy", employee.ModifiedBy);
			parameters.Add($"@DepartmentId", employee.DepartmentId);
			// Call Procedure
			string storedProcedureName = $"Proc_Employee_Insert";

			// Execute the stored procedure
			return await _dbContext.Connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);
		}

		/// <summary>
		///  Update Employee by Id
		/// </summary>
		/// <param name="employee">Employee to Update </param>
		/// <returns>nummber of record affected</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2023/12/2
		public async override Task<int> UpdateAsync(Employee employee)
		{
			var parameters = new DynamicParameters();
			parameters.Add($"@EmployeeId", employee.EmployeeId);
			parameters.Add($"@EmployeeCode", employee.EmployeeCode);
			parameters.Add($"@EmployeeName", employee.EmployeeName);
			parameters.Add($"@BirthDate", employee.BirthDate);
			parameters.Add($"@Gender", employee.Gender);
			parameters.Add($"@Identity", employee.Identity);
			parameters.Add($"@ProvideDate", employee.ProvideDate);
			parameters.Add($"@ProvidePlace", employee.ProvidePlace);
			parameters.Add($"@Address", employee.Address);
			parameters.Add($"@MobilePhone", employee.MobilePhone);
			parameters.Add($"@LandLine", employee.LandLine);
			parameters.Add($"@Email", employee.Email);
			parameters.Add($"@BankAccount", employee.BankAccount);
			parameters.Add($"@BankName", employee.BankName);
			parameters.Add($"@Branch", employee.Branch);
			parameters.Add($"@CreatedDate", employee.CreatedDate);
			parameters.Add($"@CreatedBy", employee.CreatedBy);
			parameters.Add($"@ModifiedDate", employee.ModifiedDate);
			parameters.Add($"@PositionName", employee.PositionName);
			parameters.Add($"@ModifiedBy", employee.ModifiedBy);
			parameters.Add($"@DepartmentId", employee.DepartmentId);
			// Call Procedure
			string storedProcedureName = $"Proc_Employee_Update";
			// Execute the stored procedure
			return await _dbContext.Connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);
		}

		/// <summary>
		/// Get biggest employeeCode
		/// </summary>
		/// <returns>
		/// biggest employee Code
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2024/1/21
		public async Task<string> GetBiggestEmployeeCodeAsync()
		{
			var sql = "Proc_Employee_GetMaxCode";
			return await _dbContext.Connection.QueryFirstOrDefaultAsync<string>(sql, commandType: System.Data.CommandType.StoredProcedure);
		}

		/// <summary>
		/// Check is Employee is exist or not
		/// </summary>
		/// <param name="code"> Employee Code</param>
		/// <returns>
		/// true- Employee Code exist
		/// false- Employee Code doesn't exist
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2024/1/8
		public async Task<bool> IsEmployeeCodeExistAsync(string code)
		{
			var sql = "Proc_Employee_GetEmployeeByCode";
			var paramters = new DynamicParameters();
			paramters.Add("@employeeCode", code);
			var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Employee>(sql, paramters, commandType: System.Data.CommandType.StoredProcedure, transaction: _dbContext.Transaction);
			return res != null;
		}

		/// <summary>
		/// Check is Employee email is exist or not
		/// </summary>
		/// <param name="email"> Employee email </param>
		/// <returns>
		/// true- Employee email exist
		/// false- Employee email doesn't exist
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2024/1/9
		public bool IsEmployeeEmailExist(string email)
		{
			var sql = "Proc_Empoyee_GetEmployeeByEmail";
			var paramters = new DynamicParameters();
			paramters.Add("@email", email);
			var res = _dbContext.Connection.QueryFirstOrDefault<Employee>(sql, paramters, commandType: System.Data.CommandType.StoredProcedure, transaction: _dbContext.Transaction);
			return res != null;
		}

		/// <summary>
		/// Check is Employee phone is exist or not
		/// </summary>
		/// <param name="phone"> Employee phone number </param>
		/// <returns>
		/// true- Employee phone exist
		/// false- Employee phone doesn't exist
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2024/1/9
		public bool IsEmployeePhoneExist(string phone)
		{
			var sql = "Proc_Employee_GetEmployeeByMobilePhone";
			var paramters = new DynamicParameters();
			paramters.Add("@mobilePhone", phone);
			var res = _dbContext.Connection.QueryFirstOrDefault<Employee>(sql, paramters, commandType: System.Data.CommandType.StoredProcedure, transaction: _dbContext.Transaction);
			return res != null;
		}

		/// <summary>
		///  Get recoreds to paging by offset and next
		/// </summary>
		/// <param name="offSet">Record start </param>
		/// <param name="next">Number of record have to get </param>
		/// <param name="key">search key </param>
		/// <returns>List T was paging</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2023/12/2
		public async override Task<List<Employee>> PagingAsync(int offSet, int next, string key)
		{
			var sql = "Proc_Employee_FilterPaging";
			var paramters = new DynamicParameters();
			paramters.Add("@pageSize", next);
			paramters.Add("@offSet", offSet);
			paramters.Add("@searchKey", key);
			var dataList = await _dbContext.Connection.QueryAsync<Employee>(sql, paramters, commandType: System.Data.CommandType.StoredProcedure);
			return dataList.ToList();
		}

		/// <summary>
		///  Get number of page base table and page size
		/// </summary>
		/// <param name="pageSize">Record start </param>
		/// <param name="key">search key </param>
		/// <returns>List T was paging</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2023/12/2
		public async override Task<int> GetPageSizeAsync<Employee>(int pageSize, string key)
		{

			var sql = $"Proc_Emploee_TotalPageSize";
			var paramters = new DynamicParameters();
			paramters.Add("@searchKey", key);
			var countRecord = await _dbContext.Connection.QuerySingleAsync<int>(sql, paramters, commandType: System.Data.CommandType.StoredProcedure);
			return (int)Math.Ceiling((double)countRecord / pageSize);
		}

		/// <summary>
		///  Get number of page base table and page size
		/// </summary>
		/// <returns>Total employee record in DB</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2023/12/2
		public async Task<int> CountTotalRecord<Employee>()
		{
			var sql = "SELECT COUNT(*) from Employee";
			var countRecord = await _dbContext.Connection.QuerySingleAsync<int>(sql);
			return countRecord;
		}
	}
}
