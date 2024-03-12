using CleanArchitecture.Core.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entities
{
	public class Employee
	{
		/// <summary>
		/// Employee's Identify Code
		/// </summary>
		public Guid? EmployeeId { get; set; }

		/// <summary>
		/// Employee's Code
		/// </summary>
		public string EmployeeCode { get; set; }

		/// <summary>
		/// Employees's Full name
		/// </summary>
		public string EmployeeName { get; set; }

		/// <summary>
		/// Employees's birth date
		/// </summary>
		public DateTime? BirthDate { get; set; }

		/// <summary>
		/// Employees's gender
		/// </summary>
		public int? Gender { get; set; }

		/// <summary>
		/// Employees's identity
		/// </summary>
		public string? Identity { get; set; }

		/// <summary>
		/// Identity's provide date 
		/// </summary>
		public DateTime? ProvideDate { get; set; }

		/// <summary>
		/// Identity's provide place
		/// </summary>
		public string? ProvidePlace { get; set; }

		/// <summary>
		/// Employees's Address
		/// </summary>
		public string? Address { get; set; }

		/// <summary>
		/// Employees's mobile phone number
		/// </summary>
		public string? MobilePhone { get; set; }


		/// <summary>
		/// Employees's landline number
		/// </summary>
		public string? LandLine { get; set; }

		/// <summary>
		/// Employees's email
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// Employees's bank account
		/// </summary>
		public string? BankAccount { get; set; }

		/// <summary>
		/// Employees's Bank name
		/// </summary>
		public string? BankName { get; set; }

		/// <summary>
		/// Bank's branch
		/// </summary>
		public string? Branch { get; set; }

		/// <summary>
		/// Record's create date
		/// </summary>
		public DateTime? CreatedDate { get; set; }

		/// <summary>
		/// create user
		/// </summary>
		public string? CreatedBy { get; set; }

		/// <summary>
		/// record's modify date
		/// </summary>
		public DateTime? ModifiedDate { get; set; }

		/// <summary>
		/// modify user
		/// </summary>
		public string? ModifiedBy { get; set; }

		/// <summary>
		/// Employees's Position Name
		/// </summary>
		public string? PositionName { get; set; }

		/// <summary>
		/// Employees's Dempartment identify Code
		/// </summary>
		public Guid? DepartmentId { get; set; }

		/// <summary>
		/// Employees's Dempartment name
		/// </summary>
		public string? DepartmentName { get; set; }

	}
}
