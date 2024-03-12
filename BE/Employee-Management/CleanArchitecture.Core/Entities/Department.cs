using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entities
{
	public class Department
	{
		public Department()
		{
			Employees = new HashSet<Employee>();
		}

		/// <summary>
		/// Department's Identify Code
		/// </summary
		public Guid DepartmentId { get; set; }

		/// <summary>
		/// Department's Code
		/// </summary>
		public string? DepartmentCode { get; set; }

		/// <summary>
		/// Department's Name
		/// </summary>
		public string? DepartmentName { get; set; }

		/// <summary>
		/// Create Date
		/// </summary>
		public DateTime? CreatedDate { get; set; }

		/// <summary>
		/// Create user
		/// </summary>
		public string? CreatedBy { get; set; }

		/// <summary>
		/// Moddify date
		/// </summary>
		public DateTime? ModifiedDate { get; set; }

		/// <summary>
		/// Modidy usesr
		/// </summary>
		public string? ModifiedBy { get; set; }

		/// <summary>
		/// Employee list
		/// </summary>
		public virtual ICollection<Employee>? Employees { get; set; }

	}
}
