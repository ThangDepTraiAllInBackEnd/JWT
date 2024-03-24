using CleanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exeptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Xml.Linq;

namespace CleanArchitecture.Controllers
{

    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeService employeeService, IEmployeeRepository employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }



        [HttpGet("/{id}")]
        public async Task<IActionResult> haha(Guid id)
        {
            ServiceResult res = await _employeeService.GetByIdServiceAsync(id);
            return StatusCode((int)res.Code, res);
        }

        /// <summary>
        /// get all employee from database
        /// </summary>
        /// <returns>employee List with Json format</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _employeeRepository.GetAllAsync();
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

        /// <summary>
        /// recevice Employee and insert to Database 
        /// </summary>
        /// <param name="employee">employee to insert</param>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] Employee employee)
        {
            ServiceResult res = await _employeeService.InsertServiceAsync(employee);
            return StatusCode((int)res.Code, res);
        }

        /// <summary>
        /// recevice employee and insert to Database 
        /// </summary>
        /// <param name="employee">employee to update</param>
        /// <returns>Number of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            ServiceResult res = await _employeeService.UpdateServiceAsync(employee);
            return StatusCode((int)res.Code, res);
        }

        /// <summary>
        /// Delete Employee just received 
        /// </summary>
        /// <param name="employee">employee to Delete</param>
        /// <returns>Number of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Employee employee)
        {
            ServiceResult res = await _employeeService.DeleteServiceAsync(employee);
            return StatusCode((int)res.Code, res);
        }

        /// <summary>
        /// get new employee Code to insert
        /// </summary>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        [HttpGet("/api/v1/Employee/NewEmployeeCode")]
        public async Task<IActionResult> GetNewCode()
        {
            var res = await _employeeService.CalEmployeeCodeAsync();
            return StatusCode((int)res.Code, res);
        }

        /// <summary>
        /// Get employee's total record
        /// </summary>
        /// <returns>Number of record valid and not valid</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/1/13
        [HttpGet("/api/v1/Employee/TotalEmployee")]
        public async Task<IActionResult> GetTotalRecord()
        {
            var res = await _employeeService.GetTotalRecordAsync();
            return StatusCode((int)res.Code, res);
        }

        /// <summary>
        /// Delete Employees just received 
        /// </summary>
        /// <param name="employees">employee list to delete</param>
        /// <returns>Number of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        [HttpDelete("/api/v1/Employee/MultipleDelete")]
        public async Task<IActionResult> MultipleDelete([FromBody] List<Guid?> employeesId)
        {
            ServiceResult res = await _employeeService.MultipleDeleteAsync(employeesId);
            return StatusCode((int)res.Code, res);
        }

        /// <summary>
        /// Paging entity base pageNo and pageSize and search key words
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="size">Page size</param>
        /// <param name="key">key to search for</param>
        /// <returns>Customer list was paging with Json format</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/15/1
        [HttpGet("/api/v1/Employee/Paging")]
        public async Task<IActionResult> PagingAndSearch(
        [FromQuery(Name = "page")] int page = 1,
         [FromQuery(Name = "size")] int size = 10,
         [FromQuery(Name = "key")] string? key = null)
        {
            var searchValue = "";
            if (key != null)
            {
                searchValue = key;
            }
            ServiceResult res = await _employeeService.PagingServiceAsync(page, size, searchValue.Trim());
            if (res.Data != null)
            {
                return Ok(res);
            }
            throw new InternalServerErrorCustomException(Core.Resources.MsgResource_VN.CommonErrr);
        }

        /// <summary>
        /// recevice Excel file from client 
        /// </summary>
        /// <param name="excelFile">file to preview</param>
        /// <returns>Number of record valid and not valid</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/1/13
        [HttpPost("/api/v1/Employee/PreviewExcelFile")]
        public async Task<IActionResult> PreviewExcelFile(IFormFile? excelFile)
        {
            var res = await _employeeService.PreviewFileAsync(excelFile);
            return StatusCode((int)res.Code, res);
        }

        /// <summary>
        /// exprot Excel base current page, page size and search key word
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="size">Page size</param>
        /// <param name="key">search key word</param>
        /// <returns>Number of record valid and not valid</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/1/13
        [HttpGet("/api/v1/Employee/ExportExcelFile")]
        public async Task<IActionResult> ExportExcelFile(
            [FromQuery(Name = "page")] int page = 1,
         [FromQuery(Name = "size")] int size = 10,
         [FromQuery(Name = "key")] string? key = null
            )
        {
            var searchValue = "";
            if (key != null)
            {
                searchValue = key;
            }
            var result = await _employeeService.ExportFileAsync(page, size, searchValue, "", 1);

            // Export excelFile sucess and data exist
            if (result.Success && result.Data != null)
            {
                var excelData = (Dictionary<string, object>)result.Data;
                var fileBytes = (byte[])excelData["FileBytes"];
                var fileName = (string)excelData["FileName"];
                //return excel 
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName, true);
            }   // Export error
            else
            {
                throw new InternalServerErrorCustomException(Core.Resources.MsgResource_VN.CommonErrr);
            }
        }

        /// <summary>
        /// recvie key form client and insert all valid employee from cache to db
        /// </summary>
        /// <param name="key">cache key to get cache</param>
        /// <returns>Number of record valid and not valid</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/2/1
        [HttpPost("/api/v1/Employee/ImportExcelFile")]
        public async Task<IActionResult> ImportFile([FromBody] string key)
        {
            var res = await _employeeService.ImportFileAsync(key);
            return StatusCode((int)res.Code, res);
        }


        /// <summary>
        /// Get sample excel file to import employee
        /// </summary>
        /// <returns>Sample employee excel file to import</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/3/20
        [HttpGet("/api/v1/Employee/GetSampleExcelFile")]
        public async Task<IActionResult> GetSampleFile()
        {

            var result = await _employeeService.GetSampleExcelFileAsync();

            // Export excelFile sucess and data exist
            if (result.Success && result.Data != null)
            {
                var excelData = (Dictionary<string, object>)result.Data;
                var fileBytes = (byte[])excelData["FileBytes"];
                var fileName = (string)excelData["FileName"];


                //return excel 
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName, true);
            }   // Export error
            else
            {
                throw new InternalServerErrorCustomException(Core.Resources.MsgResource_VN.CommonErrr);
            }
        }

        /// <summary>
        /// Get Error or Succes employee excel file form previous preview base key
        /// </summary>
        /// <returns>Error or success employee excel file form previous preview</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/3/21
        [HttpPost("/api/v1/Employee/GetEmployeeExcelFileBaseKey")]
        public async Task<IActionResult> GetErrorOrSuccessExcelFile([FromBody] string key)
        {
            var result = await _employeeService.ExportFileAsync(0, 0, "", key, 2);
            // Export excelFile sucess and data exist
            if (result.Success && result.Data != null)
            {
                var excelData = (Dictionary<string, object>)result.Data;
                var fileBytes = (byte[])excelData["FileBytes"];
                var fileName = (string)excelData["FileName"];


                //return excel 
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName, true);
            }   // Export error
            else
            {
                throw new InternalServerErrorCustomException(Core.Resources.MsgResource_VN.CommonErrr);
            }
        }
    }
}
