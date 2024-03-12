using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
	public class DepartmentRepository : DBContext<Department>, IDepartmentRepository
	{
		public DepartmentRepository(IDBContext dbContext) : base(dbContext)
		{
		}

		#region Method
		/// <summary>
		/// get Department by department name
		/// </summary>
		/// <param name="departmentName">name to find by</param>
		/// <returns> 
		/// department founded -> department
		/// not fount-> nulll
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public async Task<Department> GetByDepartmentNameAsync(string departmentName)
		{
			var sql = "Proc_Department_GetByDepartmentName";
			var paramters = new DynamicParameters();
			paramters.Add("@departmentName", departmentName);
			var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Department>(sql, paramters, commandType: System.Data.CommandType.StoredProcedure, transaction: _dbContext.Transaction);
			return res;
		}
		#endregion
	}
}
