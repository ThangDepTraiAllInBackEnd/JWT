using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services
{
	public class DepartmentService : BaseService<Department>, IDepartmentService
	{

		IDepartmentRepository _departmentRepository;
		public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
		{
			_departmentRepository = departmentRepository;
		}
	}
}
