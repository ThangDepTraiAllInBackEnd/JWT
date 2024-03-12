using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs
{
	public class EmployeeImport : Employee
	{
		public EmployeeImport()
		{
			this.ErrorList = new List<string>();
		}
		/// <summary>
		/// check employee valid to insert or not
		/// </summary>
		public bool? IsEmployeeValid { get; set; }
		/// <summary>
		///  check is employee insert success or not
		/// </summary>
		public bool? IsInsertSuccess { get; set; }

		/// <summary>
		/// Import key
		/// </summary>
		public string? ImportKey { get; set; }

		/// <summary>
		/// Error's list 
		/// </summary>
		public List<string>? ErrorList { get; set; }
		/// <summary>
		/// employee's Department name
		/// </summary>
		public string? DepartmentName { get; set; }
	}
}
