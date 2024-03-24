using CleanArchitecture.Core.Auth;
using CleanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Exeptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{

	[Route("api/v1/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		IDepartmentService _departmentService;
		IDepartmentRepository _departmentRepository;
		public DepartmentController(IDepartmentService departmentService, IDepartmentRepository departmentRepository)
		{
			_departmentService = departmentService;
			_departmentRepository = departmentRepository;
		}

		/// <summary>
		/// Get all Department 
		/// </summary>
		/// <returns>Department list with Json format</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/15/1
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var res = await _departmentRepository.GetAllAsync();
			if (res == null)
			{
				throw new BadRequestCustomException(CleanArchitecture.Core.Resources.MsgResource_VN.GetErr);
			}
			if (res.Count == 0)
			{
				return StatusCode(204, res);
			}
			ServiceResult response = new ServiceResult();
			response.Success = true;
			response.Data = res;
			response.Code = System.Net.HttpStatusCode.OK;
			response.UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess;
			return Ok(response);
		}
	}
}
