using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs
{
	public class Page<T>
	{
		/// <summary>
		/// List of object's record
		/// </summary>
		public List<T> ListRecord { get; set; }
		/// <summary>
		/// Total page base paging condition ( table name, prop's value in table )
		/// </summary>
		public int TotalPage { get; set; }
		/// <summary>
		/// current page
		/// </summary>
		public int CurrentPage { get; set; }
	}
}
