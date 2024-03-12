using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IEmployeeRepository Employee { get; }
		IDepartmentRepository Department { get; }
		/// <summary>
		/// Begin a Trasaction
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2023/11/9
		/// </summary>
		void BeginTransaction();
		/// <summary>
		/// Commit a Trasaction
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2023/11/9
		/// </summary>
		void Commit();
		/// <summary>
		/// Roll back Trasaction's data
		/// created by: Nguyễn Thiện Thắng
		/// created date: 2023/11/9
		/// </summary>
		void RollBack();
	}
}
