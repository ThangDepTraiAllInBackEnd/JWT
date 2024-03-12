using CleanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
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
		Task<Department> GetByDepartmentNameAsync(string departmentName);
	}
}
