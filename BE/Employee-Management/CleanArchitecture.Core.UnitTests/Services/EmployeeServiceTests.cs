using AutoMapper;
using CleanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exeptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Resources;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.UnitTests.Services
{
	[TestFixture]
	public class EmployeeServiceTests
	{

		public IEmployeeRepository EmployeeRepository { get; set; }
		public IUnitOfWork UnitOfWork { get; set; }
		public IMapper Mapper { get; set; }
		public ICacheRepository CacheRepository { get; set; }
		public EmployeeService EmployeeService { get; set; }


		[SetUp]
		public void SetUp()
		{
			EmployeeRepository = Substitute.For<IEmployeeRepository>();
			UnitOfWork = Substitute.For<IUnitOfWork>();
			Mapper = Substitute.For<IMapper>();
			CacheRepository = Substitute.For<ICacheRepository>();
			EmployeeService = Substitute.For<EmployeeService>(EmployeeRepository, UnitOfWork, Mapper, CacheRepository);
		}


		/// <summary>
		/// get employee by exist employeeId
		/// </summary>
		/// <returns>True - when employee exist</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task GetByIdServiceAsync_ExistEmployeeId_SuccessEqualsTrue()
		{
			// arrange
			var employeeId = new Guid();
			Employee employee = new Employee();
			// act
			EmployeeRepository.GetByIdAsync(employeeId).Returns(employee);
			ServiceResult? result = await EmployeeService.GetByIdServiceAsync(employeeId);
			// assert
			Assert.That(result.Success == true);
		}


		/// <summary>
		/// get employee by not exist employeeId
		/// </summary>
		/// <returns>True - when employee not exist</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task GetByIdServiceAsync_NotExistEmployeeId_SuccessEqualsFalse()
		{
			// arrange
			var employeeId = new Guid();
			Employee employee = new Employee();
			employee = null;
			// act
			EmployeeRepository.GetByIdAsync(employeeId).Returns(employee);
			ServiceResult? result = await EmployeeService.GetByIdServiceAsync(employeeId);
			// assert
			Assert.That(result.Success == false);
		}

		/// <summary>
		/// insert valid employee
		/// </summary>
		/// <returns>True - when insert success</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task InsertServiceAsync_ValidEmployeeToInsert_SuccessEqualsTrue()
		{
			// arrange
			Employee employee = new Employee();
			// act
			EmployeeService.BeforeInsertAsync(employee).Returns(true);
			UnitOfWork.Employee.InsertAsync(employee).Returns(1);
			EmployeeService.When(substituteCall: x => x.InsertServiceAsync(Arg.Any<Employee>())).CallBase();
			ServiceResult? result = await EmployeeService.InsertServiceAsync(employee);
			// assert
			Assert.That(result.Success == true);
		}

		/// <summary>
		/// insert invalid employee
		/// </summary>
		/// <returns>True - when insert failed</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task InsertServiceAsync_InValidEmployeeToInsert_SuccessEqualsFalse()
		{
			// arrange
			Employee employee = new Employee();
			// act
			EmployeeService.BeforeInsertAsync(employee).Returns(false);
			UnitOfWork.Employee.InsertAsync(employee).Returns(1);
			EmployeeService.When(substituteCall: x => x.InsertServiceAsync(Arg.Any<Employee>())).CallBase();
			ServiceResult? result = await EmployeeService.InsertServiceAsync(employee);
			// assert
			Assert.That(result.Success == false);
		}

		/// <summary>
		/// update valid employee
		/// </summary>
		/// <returns>True - when update success</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task UpdateServiceAsync_ValidEmployeeToUpdate_SuccessEqualsTrue()
		{
			// arrange
			Employee employee = new Employee();
			// act
			EmployeeService.BeforeUpdateAsync(employee).Returns(true);
			UnitOfWork.Employee.UpdateAsync(employee).Returns(1);
			EmployeeService.When(substituteCall: x => x.UpdateServiceAsync(Arg.Any<Employee>())).CallBase();
			ServiceResult? result = await EmployeeService.UpdateServiceAsync(employee);
			// assert
			Assert.That(result.Success == true);
		}

		/// <summary>
		/// update inValid employee
		/// </summary>
		/// <returns>True - when update success</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task UpdateServiceAsync_InValidEmployeeToUpdate_SuccessEqualsFalse()
		{
			// arrange
			Employee employee = new Employee();
			// act
			EmployeeService.BeforeUpdateAsync(employee).Returns(false);
			UnitOfWork.Employee.UpdateAsync(employee).Returns(1);
			EmployeeService.When(substituteCall: x => x.UpdateServiceAsync(Arg.Any<Employee>())).CallBase();
			ServiceResult? result = await EmployeeService.UpdateServiceAsync(employee);
			// assert
			Assert.That(result.Success == false);
		}


		/// <summary>
		/// delete exist employee
		/// </summary>
		/// <returns>True - when delete success</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task DeleteServiceAsync_ExistEmployee_SuccessEqualsTrue()
		{
			// arrange
			Employee employee = new Employee();
			// act
			EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(employee);
			EmployeeRepository.DeleteAsync(employee).Returns(1);
			EmployeeService.When(substituteCall: x => x.DeleteServiceAsync(Arg.Any<Employee>())).CallBase();
			ServiceResult? result = await EmployeeService.DeleteServiceAsync(employee);
			// assert
			Assert.That(result.Success == true);
		}

		/// <summary>
		/// delete not exist employee
		/// </summary>
		/// <returns>True - when delete Failed</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task DeleteServiceAsync_NotExistEmployee_SuccessEqualsFalse()
		{
			// arrange
			Employee employee = new Employee();
			Employee notExistEmployee = null;
			// act
			EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(notExistEmployee);
			EmployeeRepository.DeleteAsync(employee).Returns(1);
			EmployeeService.When(substituteCall: x => x.DeleteServiceAsync(Arg.Any<Employee>())).CallBase();
			ServiceResult? result = await EmployeeService.DeleteServiceAsync(employee);
			// assert
			Assert.That(result.Success == false);
		}

		/// <summary>
		/// check is employee valid to insert
		/// </summary>
		/// <returns>True - when employee valid to insert</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task BeforeInsertAsync_ValidEmployee_True()
		{
			// arrange
			Employee employee = new Employee()
			{
				EmployeeName = "Test",
			};
			Employee notExistEmployee = null;
			// act
			EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(notExistEmployee);
			EmployeeRepository.IsEmployeeCodeExistAsync(employee.EmployeeCode).Returns(false);
			EmployeeService.When(substituteCall: x => x.BeforeInsertAsync(Arg.Any<Employee>())).CallBase();
			var result = await EmployeeService.BeforeInsertAsync(employee);
			// assert
			Assert.That(result == true);
		}

		/// <summary>
		/// check is employee valid to insert
		/// </summary>
		/// <returns>True - when employee invalid to insert</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task BeforeInsertAsync_ExistEmployee_False()
		{
			// arrange
			Employee employee = new Employee()
			{
				EmployeeName = "Test",
			};
			// act
			EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(employee);
			EmployeeRepository.IsEmployeeCodeExistAsync(employee.EmployeeCode).Returns(false);
			EmployeeService.When(substituteCall: x => x.BeforeInsertAsync(Arg.Any<Employee>())).CallBase();
			var result = await EmployeeService.BeforeInsertAsync(employee);
			// assert
			Assert.That(result == false);
		}

		/// <summary>
		/// check is employee valid to insert
		/// </summary>
		/// <returns>True - when employee invalid to insert</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public void BeforeInsertAsync_EmployeeWithCodeDuplication_ValidateException()
		{
			// arrange
			Employee employee = new Employee()
			{
				EmployeeName = "Test",
			};
			Employee notExistEmployee = null;
			// act
			EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(notExistEmployee);
			EmployeeRepository.IsEmployeeCodeExistAsync(employee.EmployeeCode).Returns(true);
			EmployeeService.When(substituteCall: x => x.BeforeInsertAsync(Arg.Any<Employee>())).CallBase();
			var ex = Assert.ThrowsAsync<BadRequestCustomException>(async () => await EmployeeService.BeforeInsertAsync(employee));
			// assert
			Assert.That(ex.Message, Is.EqualTo($"{Resources.MsgResource_VN.EmpCodeDoNotExistFront}{employee.EmployeeCode}{Resources.MsgResource_VN.EmpCodeDoNotExistBack}"));
		}

		/// <summary>
		/// check is employee valid to insert
		/// </summary>
		/// <returns>True - when employee invalid to insert</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public void BeforeInsertAsync_EmployeeWithoutName_ValidateException()
		{
			// arrange
			Employee employee = new Employee();
			Employee notExistEmployee = null;
			// act
			EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(notExistEmployee);
			EmployeeRepository.IsEmployeeCodeExistAsync(employee.EmployeeCode).Returns(false);
			EmployeeService.When(substituteCall: x => x.BeforeInsertAsync(Arg.Any<Employee>())).CallBase();
			var ex = Assert.ThrowsAsync<BadRequestCustomException>(async () => await EmployeeService.BeforeInsertAsync(employee));
			// assert
			Assert.That(ex.Message, Is.EqualTo(Resources.MsgResource_VN.EmployeeNameCannotBeNull));
		}

		/// <summary>
		/// check is employee valid to insert
		/// </summary>
		/// <returns>True - when employee invalid to update</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task BeforeUpdateAsync_NotExistEmployee_False()
		{
			// arrange
			Employee employee = new Employee();
			Employee notExistEmployee = null;
			// act
			EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(notExistEmployee);
			EmployeeRepository.IsEmployeeCodeExistAsync(employee.EmployeeCode).Returns(false);
			EmployeeService.When(substituteCall: x => x.BeforeUpdateAsync(Arg.Any<Employee>())).CallBase();
			var result = await EmployeeService.BeforeUpdateAsync(employee);
			// assert
			Assert.That(result == false);
		}

		/// <summary>
		/// check is employee valid to insert
		/// </summary>
		/// <returns>True - when employee valid to update</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task BeforeUpdateAsync_EmployeeWithExistingEmployeeCodeAndMatchWithEmployeeCodeInDb_True()
		{
			// arrange
			Employee employee = new Employee();
			Employee employeeInDb = new Employee();
			// act
			EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(employee);
			EmployeeRepository.IsEmployeeCodeExistAsync(employee.EmployeeCode).Returns(true);
			EmployeeService.When(substituteCall: x => x.BeforeUpdateAsync(Arg.Any<Employee>())).CallBase();
			var result = await EmployeeService.BeforeUpdateAsync(employee);
			// assert
			Assert.That(result == true);
		}

		/// <summary>
		/// check is employee valid to insert
		/// </summary>
		/// <returns>True - when employee valid to update</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public void BeforeUpdateAsync_EmployeeWithExistingEmployeeCodeAndNotMatchWithEmployeeCodeInDb_True()
		{
			// arrange
			Employee employee = new Employee()
			{
				EmployeeCode = "NV-000000"
			};
			Employee employeeInDb = new Employee()
			{
				EmployeeCode = "NV-000001"
			};
			// act
			EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(employeeInDb);
			EmployeeRepository.IsEmployeeCodeExistAsync(employee.EmployeeCode).Returns(true);
			EmployeeService.When(substituteCall: x => x.BeforeUpdateAsync(Arg.Any<Employee>())).CallBase();
			var ex = Assert.ThrowsAsync<BadRequestCustomException>(async () => await EmployeeService.BeforeUpdateAsync(employee));
			// assert
			Assert.That(ex.Message, Is.EqualTo(Resources.MsgResource_VN.EmployeeDoNotExist));
		}

		/// <summary>
		/// Calculate to get bigger employeeCode
		/// </summary>
		/// <returns>True - when no record in db and return NV-000000</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task CalEmployeeCodeAsync_None_DataEqualsNV000000()
		{
			// act
			EmployeeRepository.GetBiggestEmployeeCodeAsync().Returns(Task.FromResult<string?>(null));
			ServiceResult? result = await EmployeeService.CalEmployeeCodeAsync();
			// assert
			Assert.That(result.Data.Equals("NV-000000"));
		}

		/// <summary>
		/// Calculate to get bigger employeeCode
		/// </summary>
		/// <returns>True - when there one record in db and return NV-000001</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task CalEmployeeCodeAsync_None_DataEqualsNV000001()
		{
			//act
			EmployeeRepository.GetBiggestEmployeeCodeAsync().Returns("NV-000000");
			ServiceResult? result = await EmployeeService.CalEmployeeCodeAsync();
			// assert
			Assert.That(result.Data.Equals("NV-000001"));
		}

		/// <summary>
		/// Paging records base page, pageSize and search key
		/// </summary>
		/// <returns>True - when current page < 1 and throws exception</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task PagingServiceAsync_CurrentPage_PageSize_SearchKey_ValidateException()
		{
			//arrange
			int currentPage = 0;
			int pageSize = 10;
			string searchKey = "";
			//act 
			EmployeeService.When(substituteCall: x => x.PagingServiceAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>())).CallBase();

			var ex = Assert.ThrowsAsync<BadRequestCustomException>(async () => await EmployeeService.PagingServiceAsync
			(currentPage, pageSize, searchKey));
			// assert
			Assert.That(ex.Message, Is.EqualTo(Resources.MsgResource_VN.PagingErr));
		}


		/// <summary>
		/// Paging records base page, pageSize and search key
		/// </summary>
		/// <returns>True - when current page < 1 and throws exception</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task PagingServiceAsync_CurrentPage_PageSize_SearchKey_SuccessEqualsTrue()
		{
			//arrange
			int currentPage = 1;
			int pageSize = 10;
			string searchKey = "";
			List<Employee> pagingList = new List<Employee>();
			pagingList.Add(new Employee());
			//act 
			EmployeeRepository.PagingAsync((currentPage - 1) * pageSize, pageSize, searchKey).Returns(pagingList);
			EmployeeService.When(substituteCall: x => x.PagingServiceAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>())).CallBase();

			ServiceResult result = await EmployeeService.PagingServiceAsync(currentPage, pageSize, searchKey);
			// assert
			Assert.That(result.Success == true);
		}

		/// <summary>
		/// Delete employess by EmployeeId list
		/// </summary>
		/// <returns>True - when employee list is empty and throws exception</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public void DeleteManyAsync_EmptyEmployeeList_ValidateException()
		{
			//arrange
			List<Employee> deleteEmployees = new List<Employee>();
			var ex = Assert.ThrowsAsync<BadRequestCustomException>(async () => await EmployeeService.MultipleDeleteAsync
		(deleteEmployees));
			// assert
			Assert.That(ex.Message, Is.EqualTo(Resources.MsgResource_VN.EmptyEmployeeList));
		}

		/// <summary>
		/// Delete employess by EmployeeId list
		/// </summary>
		/// <returns>True - when empployees were deleted success</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task DeleteManyAsync_ValidEmployeeList_SuccessIsEqualsTrue()
		{
			//arrange
			List<Employee> deleteEmployees = new List<Employee>();
			deleteEmployees.Add(new Employee());
			EmployeeRepository.GetByIdAsync(deleteEmployees[0].EmployeeId).Returns(deleteEmployees[0]);
			EmployeeRepository.DeleteAsync(deleteEmployees[0]).Returns(1);
			var result = await EmployeeService.MultipleDeleteAsync(deleteEmployees);
			// assert
			Assert.That(result.Success == true);
		}

		/// <summary>
		/// Delete employess by EmployeeId list
		/// </summary>
		/// <returns>True - when employees were deleted faild and throws validate exception</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public void DeleteManyAsync_ValidEmployeeList_ValidateException()
		{
			//arrange
			List<Employee> deleteEmployees = new List<Employee>();
			deleteEmployees.Add(new Employee());
			EmployeeRepository.GetByIdAsync(deleteEmployees[0].EmployeeId).Returns(deleteEmployees[0]);
			EmployeeRepository.DeleteAsync(deleteEmployees[0]).Returns(0);
			var ex = Assert.ThrowsAsync<BadRequestCustomException>(async () => await EmployeeService.MultipleDeleteAsync
		(deleteEmployees));
			// assert
			Assert.That(ex.Message, Is.EqualTo(Resources.MsgResource_VN.DeleteErr));
		}

		/// <summary>
		/// Delete employess by EmployeeId list
		/// </summary>
		/// <returns>True - when employees were deleted faild and throws validate exception</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public void DeleteManyAsync_EmployeeListWithNotFoundEmployee_ValidateException()
		{
			//arrange
			List<Employee> deleteEmployees = new List<Employee>();
			deleteEmployees.Add(new Employee());
			Employee notFoundEmployee = null;
			EmployeeRepository.GetByIdAsync(deleteEmployees[0].EmployeeId).Returns(notFoundEmployee);
			EmployeeRepository.DeleteAsync(deleteEmployees[0]).Returns(0);
			var ex = Assert.ThrowsAsync<BadRequestCustomException>(async () => await EmployeeService.MultipleDeleteAsync
		(deleteEmployees));
			// assert
			Assert.That(ex.Message, Is.EqualTo(Resources.MsgResource_VN.EmployeeNotFound));
		}

		/// <summary>
		///  Get total employee record in DB
		/// </summary>
		/// <returns>True - when count employee success</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/3
		[Test]
		public async Task GetTotalRecordAsync_None_SuccessIsEqualsTrue()
		{
			// Arrange
			List<Employee> employees = new List<Employee>();
			employees.Add(new Employee());
			UnitOfWork.Employee.GetAllAsync().Returns(Task.FromResult(employees));
			// Act 
			var result = await EmployeeService.GetTotalRecordAsync();
			// Assert
			Assert.That(result.Success == true);
		}
	}
}
