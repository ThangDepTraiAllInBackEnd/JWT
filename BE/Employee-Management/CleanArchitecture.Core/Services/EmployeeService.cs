using AutoMapper;
using CleanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exeptions;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services
{
	public class EmployeeService : BaseService<Employee>, IEmployeeService
	{
		IEmployeeRepository _employeeRepository;
		IUnitOfWork _unitOfWork;
		IMapper _mapper;
		ICacheRepository _cacheRepository;
		public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IMapper mapper, ICacheRepository cacheRepository) : base(employeeRepository)
		{
			_employeeRepository = employeeRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_cacheRepository = cacheRepository;
		}

		public async override Task<ServiceResult> GetAllServiceAsync()
		{
			return await base.GetAllServiceAsync();
		}

		/// <summary>
		/// Get Employee from db by Id and push to service result
		/// </summary>
		/// <param name="id">id to find by</param>
		/// <returns>Serivce result ( sucsess with data or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public async Task<ServiceResult> GetByIdServiceAsync(Guid id)
		{
			Employee? employee = await _employeeRepository.GetByIdAsync(id);
			// Employee exsist -> return 
			if (employee != null)
			{
				return new ServiceResult()
				{
					Success = true,
					Code = System.Net.HttpStatusCode.OK,
					Data = employee,
					MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess,
					UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess
				};
			}
			else
			{
				return new ServiceResult()
				{
					Success = false,
					Code = System.Net.HttpStatusCode.NotFound,
					Data = employee,
					MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeNotFound,
					UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeNotFound
				};
			}
			throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.FoundErr);
		}

		/// <summary>
		/// insert by repository || Check is all entity' properies is valid before insert
		/// </summary>
		/// <param name="employee">employee to insert </param>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/10
		public async override Task<ServiceResult> InsertServiceAsync(Employee employee)
		{
			// all employee's prop is valid to insert
			if (await BeforeInsertAsync(employee))
			{
				employee.EmployeeId = Guid.NewGuid();
				_unitOfWork.BeginTransaction();
				// insert success
				if (await _unitOfWork.Employee.InsertAsync(employee) > 0)
				{
					_unitOfWork.Commit();
					return new ServiceResult()
					{
						Success = true,
						Code = System.Net.HttpStatusCode.Created,
						Data = 1,
						MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.AddSuccess,
						UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.AddSuccess
					};
				}
			}
			// err insert
			return new ServiceResult()
			{
				Success = false,
				Code = System.Net.HttpStatusCode.BadRequest,
				Data = 0,
				MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.AddErr,
				UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.AddErr
			};
		}

		/// <summary>
		/// Check is all employee' properies is valid before Update
		/// </summary>
		/// <param name="employee">employee to update </param>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public async override Task<ServiceResult> UpdateServiceAsync(Employee employee)
		{
			if (await BeforeUpdateAsync(employee))
			{
				_unitOfWork.BeginTransaction();
				// update success
				if (await _unitOfWork.Employee.UpdateAsync(employee) > 0)
				{
					_unitOfWork.Commit();
					return new ServiceResult()
					{
						Success = true,
						Code = System.Net.HttpStatusCode.OK,
						Data = 1,
						MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.UpdateSuccess,
						UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.UpdateSuccess
					};
				}
			}
			else
			{
				return new ServiceResult()
				{
					Success = false,
					Code = System.Net.HttpStatusCode.NotFound,
					Data = 0,
					MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeNotFound,
					UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeNotFound
				};
			}
			throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.UpdateErr);
		}

		/// <summary>
		/// Check is employee id valid -> delete
		/// </summary>
		/// <param name="employee">employee to delete </param>
		/// <returns>Serivce result ( success or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public override async Task<ServiceResult> DeleteServiceAsync(Employee employee)
		{
			Employee employeeInDb = await _employeeRepository.GetByIdAsync(employee.EmployeeId);
			//entity exist
			if (employeeInDb != null)
			{
				_unitOfWork.BeginTransaction();
				// delete success
				if (await _employeeRepository.DeleteAsync(employee) > 0)
				{
					_unitOfWork.Commit();
					return new ServiceResult()
					{
						Success = true,
						Code = System.Net.HttpStatusCode.OK,
						Data = 1,
						MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.DeleteSucess,
						UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.DeleteSucess,
					};
				}
			}
			else if (employeeInDb == null)
			{
				return new ServiceResult()
				{
					Success = false,
					Code = System.Net.HttpStatusCode.NotFound,
					Data = 0,
					MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeNotFound,
					UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeNotFound
				};

			}
			throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.DeleteErr);
		}

		#region Before action
		/// <summary>
		/// Check is all Employee's poroperty valid or not before insert 
		/// </summary>
		/// <param name="employee">Employee to validate</param>
		/// <returns>
		/// true- All property is valid
		/// false- One or more property is not valid
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		///  
		public async override Task<bool> BeforeInsertAsync(Employee employee)
		{
			Employee employeeInDb = await _employeeRepository.GetByIdAsync(employee.EmployeeId);
			// is employeeId exist -> cannot insert
			if (employeeInDb != null)
			{
				return false;
			}
			// is employeeCode exist -> cannot insert
			if (await _employeeRepository.IsEmployeeCodeExistAsync(employee.EmployeeCode))
			{
				throw new ValidateExeption($"{Resources.MsgResource_VN.EmpCodeDoNotExistFront}{employee.EmployeeCode}{Resources.MsgResource_VN.EmpCodeDoNotExistBack}");
			}
			// employee name null-> cannot insert
			if (employee.EmployeeName == null || employee.EmployeeName == "")
			{
				throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeNameCannotBeNull);
			}
			return true;
		}

		/// <summary>
		/// Action before update entity into database
		/// </summary>
		/// <param name="employee">Entity to manipulate</param>
		/// <returns>
		/// true-entity valid to update
		/// false- entity valid not to update
		/// </returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public async override Task<bool> BeforeUpdateAsync(Employee employee)
		{

			Employee employeeInDb = await _employeeRepository.GetByIdAsync(employee.EmployeeId);
			// entity doesn't exist
			if (employeeInDb == null)
			{
				return false;
			}
			// Code exit -> Check to see if this Code belongs to the employee who is editing 
			// true -> update | false -> throw duplicate Code exeption
			if (await _employeeRepository.IsEmployeeCodeExistAsync(employee.EmployeeCode))
			{
				if (employee.EmployeeCode != employeeInDb.EmployeeCode)
				{
					throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeDoNotExist);
				}
			}

			return true;
		}
		#endregion

		/// <summary>
		/// Calculate to get bigger employeeCode
		/// </summary>
		/// <returns>new Employee Code/returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/21
		public async Task<ServiceResult> CalEmployeeCodeAsync()
		{
			string currentCode = await _employeeRepository.GetBiggestEmployeeCodeAsync();
			// no record in db
			if (currentCode == null)
			{
				return new ServiceResult()
				{
					Success = true,
					Code = HttpStatusCode.OK,
					Data = "NV-000000",
					MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess,
					UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess
				};
			}
			string numberPart = currentCode.Substring(3);
			// get prefix
			string prefix = currentCode.Substring(0, currentCode.Length - numberPart.Length);
			if (int.TryParse(numberPart, out int currentNumber))
			{
				// increase int number's value
				currentNumber++;

				// Format
				string newEmployeeCode = $"{prefix}{currentNumber}";
				return new ServiceResult()
				{
					Success = true,
					Code = HttpStatusCode.OK,
					Data = newEmployeeCode,
					MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess,
					UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess
				};
			}
			throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.CommonErrr);
		}
		/// <summary>
		/// Paging records base page, pageSize and search key
		/// </summary>
		/// <param name="page">Current page </param>
		///  <param name="pageSize">record'number /page </param>
		///  <param name="key">search key </param>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/15/1
		public async override Task<ServiceResult> PagingServiceAsync(int page, int pageSize, string key)
		{
			// page have to > 0
			if (page < 1)
			{
				throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.PagingErr);
			}
			List<Employee> pagingList = await _employeeRepository.PagingAsync((page - 1) * pageSize, pageSize, key);
			if (pagingList.Count >= 0)
			{
				Page<Employee> pageObject = new Page<Employee>()
				{
					ListRecord = pagingList,
					CurrentPage = page,
					TotalPage = await _employeeRepository.GetPageSizeAsync<Employee>(pageSize, key)
				};
				return new ServiceResult()
				{
					Data = new
					{
						records = pageObject,
						totalRecords = await _employeeRepository.CountTotalRecord<Employee>()
					},
					Success = true,
					Code = System.Net.HttpStatusCode.OK,
					MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess,
					UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess
				};
			}
			return new ServiceResult()
			{
				Data = null,
				Success = false,
				Code = System.Net.HttpStatusCode.BadGateway,
				MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.GetErr,
				UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.GetErr
			};
		}

		/// <summary>
		/// Delete employess by EmployeeId list
		/// </summary>
		/// <param name="employees">Employee list to delete</param>
		/// <returns>Serivce result ( sucsess with data or failed with all details )</returns>	
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public async Task<ServiceResult> DeleteManyAsync(List<Employee> employees)
		{
			// empty list
			if (employees.Count == 0 || employees == null)
			{
				throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.EmptyEmployeeList);
			}
			int countDelete = 0;
			_unitOfWork.BeginTransaction();
			// loop form the first employee to the last employee, get Id and delete
			foreach (Employee employee in employees)
			{
				// employee exist -> can be delete
				if (await _employeeRepository.GetByIdAsync(employee.EmployeeId) != null)
				{
					// delete success
					if (await _employeeRepository.DeleteAsync(employee) > 0)
					{
						countDelete++;
					}
					else
					{
						throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.DeleteErr);
					}
				}
				else
				{
					throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeNotFound);
				}
			}
			// lack delete -> not commit -> show error
			if (countDelete != employees.Count)
			{
				throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.CommonErrr);
			}
			// commit delete
			_unitOfWork.Commit();
			return new ServiceResult()
			{
				Success = true,
				Code = System.Net.HttpStatusCode.OK,
				Data = countDelete,
				UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.DeleteSucess
			};
		}

		#region FIle Action
		/// <summary>
		/// exprot file for user base user's current page, page size and search key word
		/// </summary>
		/// <param name="page">Current page </param>
		///  <param name="pageSize">record'number /page </param>
		///  <param name="key">search key </param>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/15/1
		public async Task<ServiceResult> ExportFileAsync(int page, int pageSize, string key)
		{
			try
			{
				List<Employee> employees = await _employeeRepository.PagingAsync((page - 1) * pageSize, pageSize, key);
				ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
				// create an excel file
				using (ExcelPackage package = new ExcelPackage())
				{
					// create a work sheet
					ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(Resources.CommonRs.EmployeeWorkSheetName);
					int row = 4;
					int index = 1;
					ExcelRange titleRange = worksheet.Cells["A1:I1"];
					worksheet.Cells["A2:I2"].Merge = true;
					titleRange.Merge = true;
					titleRange.Value = Resources.CommonRs.EmployeeFileHeader;
					titleRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


					using (ExcelRange rng = worksheet.Cells["A1:I1"])
					{
						rng.Style.Font.Bold = true;
						rng.Style.Font.Size = 14;
					}

					// header title
					worksheet.Cells[3, 1].Value = "STT";
					worksheet.Cells[3, 2].Value = "Mã nhân viên";
					worksheet.Cells[3, 3].Value = "Tên nhân viên";
					worksheet.Cells[3, 4].Value = "Giới tính";
					worksheet.Cells[3, 5].Value = "Ngày sinh";
					worksheet.Cells[3, 6].Value = "Chức danh";
					worksheet.Cells[3, 7].Value = "Tên đơn vị";
					worksheet.Cells[3, 8].Value = "Số tài khoản";
					worksheet.Cells[3, 9].Value = "Tên ngân hàng";
					for (int col = 1; col <= 9; col++)
					{
						worksheet.Cells[3, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
						worksheet.Cells[3, col].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
					}

					// write data
					foreach (var employee in employees)
					{
						worksheet.Cells[row, 1].Value = index;
						worksheet.Cells[row, 2].Value = employee.EmployeeCode;
						worksheet.Cells[row, 3].Value = employee.EmployeeName;
						string gender;
						switch (employee.Gender)
						{
							case 0:
								gender = "Nam";
								break;
							case 1:
								gender = "Nữ";
								break;
							default:
								gender = "Khác";
								break;
						}
						worksheet.Cells[row, 4].Value = gender;
						worksheet.Cells[row, 5].Style.Numberformat.Format = "dd/MM/yyyy";
						worksheet.Cells[row, 5].Value = employee.BirthDate;
						worksheet.Cells[row, 6].Value = employee.PositionName;
						worksheet.Cells[row, 7].Value = employee.DepartmentName;
						worksheet.Cells[row, 8].Value = employee.BankAccount;
						worksheet.Cells[row, 9].Value = employee.BankName;
						row++;
						index++;
					}

					// auto fit column's with
					worksheet.Cells.AutoFitColumns();
					MemoryStream stream = new MemoryStream(package.GetAsByteArray());
					byte[] fileBytes = stream.ToArray();
					string fileName = Resources.CommonRs.EmployeeFileName;
					var excelData = new Dictionary<string, object>
			{
				{ "FileBytes", fileBytes },
				{ "FileName", fileName }
			};

					return await Task.FromResult(new ServiceResult
					{
						Success = true,
						Code = HttpStatusCode.OK,
						Data = excelData,
						MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.UpLoadFailed,
						UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.UpLoadFailed
					});
				}
			}
			catch (Exception ex)
			{
				throw new ValidateExeption(ex.Message);
			}
		}

		/// <summary>
		/// recevie a file and save to db
		/// </summary>
		/// <param name="file">file to save to db</param>
		/// <returns>S( sucsess with data or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public async Task<ServiceResult> PreviewFileAsync(IFormFile excelFile)
		{
			if (excelFile == null || excelFile.Length == 0)
			{
				return new ServiceResult()
				{
					Success = false,
					Code = System.Net.HttpStatusCode.BadRequest,
					Data = null,
					MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.FileNotValid,
					UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.FileNotValid,
				};
			}

			// Check file extension
			string fileExtension = Path.GetExtension(excelFile.FileName);
			if (fileExtension != ".xlsx")
			{
				return new ServiceResult()
				{
					Success = false,
					Code = System.Net.HttpStatusCode.BadRequest,
					Data = null,
					MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeFile_FileWrongFormat,
					UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeFile_FileWrongFormat,
				};
			}
			try
			{
				var filePath = Path.Combine(Resources.CommonRs.UploadFolderName, excelFile.FileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await excelFile.CopyToAsync(stream);
				}
				// file exist
				if (File.Exists(filePath))
				{
					List<EmployeeImport> employeesImport = new List<EmployeeImport>();
					List<Employee> validEmployees = new();
					ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

					using (var package = new ExcelPackage(new FileInfo(filePath)))
					{
						var worksheet = package.Workbook.Worksheets[0];
						var rowCount = worksheet.Dimension.Rows;

						string[] expectedHeaders = { "STT", "Mã nhân viên", "Tên nhân viên", "Giới tính", "Ngày sinh", "Chức danh", "Tên đơn vị", "Số tài khoản", "Tên ngân hàng" };
						// check headers title are match with Demo file or not
						for (int i = 0; i < expectedHeaders.Length; i++)
						{
							// not match -> throw exption
							if (worksheet.Cells[3, i + 1].Value?.ToString()?.Trim() != expectedHeaders[i])
							{
								throw new ValidateExeption(Resources.MsgResource_VN.EmployeeFile_FileNotValid);
							}
						}
						_unitOfWork.BeginTransaction();

						for (int row = 4; row <= rowCount; row++)
						{
							EmployeeImport employeeImport = new EmployeeImport();
							employeeImport.EmployeeCode = worksheet.Cells[row, 2].Value?.ToString()?.Trim();
							employeeImport.EmployeeName = worksheet.Cells[row, 3].Value?.ToString()?.Trim();

							switch (worksheet.Cells[row, 4].Value?.ToString()?.Trim().ToLower())
							{
								case "nam":
									employeeImport.Gender = 0;
									break;
								case "nữ":
									employeeImport.Gender = 1;
									break;
								case "khác":
									employeeImport.Gender = 2;
									break;
								default:
									employeeImport.Gender = null;
									break;
							}

							employeeImport.PositionName = worksheet.Cells[row, 6].Value?.ToString()?.Trim();
							employeeImport.DepartmentName = worksheet.Cells[row, 7].Value?.ToString()?.Trim();
							employeeImport.BankAccount = worksheet.Cells[row, 8].Value?.ToString()?.Trim();
							employeeImport.BankName = worksheet.Cells[row, 9].Value?.ToString()?.Trim();
							// data is valid -> accept
							if (double.TryParse(worksheet.Cells[row, 5].Value?.ToString()?.Trim(), out double excelDate))
							{
								DateTime dateOfBirth = DateTime.FromOADate(excelDate);
								employeeImport.BirthDate = dateOfBirth;
							}

							//employyee name is required
							if (string.IsNullOrEmpty(employeeImport.EmployeeName))
							{
								employeeImport.ErrorList.Add(Resources.MsgResource_VN.EmployeeFIle_NameIsRequired);
							}
							// employee Code is required
							if (string.IsNullOrEmpty(employeeImport.EmployeeCode))
							{
								employeeImport.ErrorList.Add(Resources.MsgResource_VN.EmployeeFile_EmployeeCodeCanNotBeNull);
							}
							// employeecode duplicate -> err
							else if (await _unitOfWork.Employee.IsEmployeeCodeExistAsync(employeeImport.EmployeeCode))
							{
								employeeImport.ErrorList.Add($"{Resources.MsgResource_VN.EmpCodeDoNotExistFront}" +
															$"{employeeImport.EmployeeCode}" +
															$"{Resources.MsgResource_VN.EmpCodeDoNotExistBack}");
							}

							// department name is required
							if (string.IsNullOrEmpty(employeeImport.DepartmentName))
							{
								employeeImport.ErrorList.Add(Resources.MsgResource_VN.EmployeeFile_DepartmentNameIsRequired);
							}
							else
							{
								Department department = await _unitOfWork.Department.GetByDepartmentNameAsync(employeeImport.DepartmentName);
								// check is department name is exist or not
								if (department != null)
								{
									employeeImport.DepartmentName = department.DepartmentName;
								}
								else
								{
									employeeImport.ErrorList.Add(Resources.MsgResource_VN.EmployeeFile_DepartmentNotValid);
								}
							}
							employeeImport.IsEmployeeValid = employeeImport.ErrorList.Count == 0;
							// employee valid 
							if ((bool)employeeImport.IsEmployeeValid)
							{
								Employee employee = _mapper.Map<Employee>(employeeImport);
								validEmployees.Add(employee);
							}
							employeesImport.Add(employeeImport);
						}
						TimeSpan timeSpan = TimeSpan.FromMinutes(30);
						var cacheKey = Guid.NewGuid().ToString();
						_cacheRepository.SetCache<List<Employee>>(cacheKey, validEmployees, timeSpan);
						return new ServiceResult()
						{
							Success = true,
							Code = System.Net.HttpStatusCode.OK,
							Data = new
							{
								employeesImport = employeesImport,
								cachekey = cacheKey
							},
							MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.UploadSuccess,
							UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.UploadSuccess,
						};
					}
				}
				// updload failed
				else
				{
					return new ServiceResult()
					{
						Success = false,
						Code = System.Net.HttpStatusCode.BadRequest,
						Data = null,
						MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeFile_FileWrongFormat,
						UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeFile_FileWrongFormat,
					};
				}
			}
			catch (Exception ex)
			{
				throw new ValidateExeption(ex.Message);
			}
		}

		/// <summary>
		/// insert all valid employee base cache key from previous preview
		/// </summary>
		///  <param name="key">Cache key to get data to insert employee into db </param>
		/// <returns>Service result ( sucsess or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/2/1
		public async Task<ServiceResult> ImportFileAsync(string key)
		{
			// cache not exist
			if (_cacheRepository.IsCacheExist(key))
			{
				var employeeList = _cacheRepository.GetCache<List<Employee>>(key);
				int countInsert = 0;
				_unitOfWork.BeginTransaction();
				try
				{
					// insert all valid employee
					foreach (Employee employee in employeeList)
					{
						employee.EmployeeId = Guid.NewGuid();
						if (await _unitOfWork.Employee.InsertAsync(employee) > 0)
						{
							countInsert++;
						}
						else
						{
							throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.CommonErrr);
						}
					}
					// all employee is insert success
					if (countInsert == employeeList.Count)
					{
						_unitOfWork.Commit();
					}
					else
					{
						throw new ValidateExeption(CleanArchitecture.Core.Resources.MsgResource_VN.CommonErrr);
					}
					return new ServiceResult()
					{
						Success = true,
						Code = System.Net.HttpStatusCode.Created,
						Data = employeeList.Count,
						MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.AddSuccess,
						UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.AddSuccess,
					};
				}
				catch (Exception ex)
				{
					throw new ValidateExeption(ex.Message);
				}
			}
			return new ServiceResult()
			{
				Success = false,
				Code = System.Net.HttpStatusCode.BadRequest,
				Data = null,
				MsgResource_VN = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeFile_FileWrongFormat,
				UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.EmployeeFile_FileWrongFormat,
			};
		}
		#endregion

		/// <summary>
		/// Get total employee record in DB
		/// </summary>
		/// <returns>Serivce result ( sucsess with data or failed with all details )</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/1/9
		public async Task<ServiceResult> GetTotalRecordAsync()
		{
			var totalRecord = (await _unitOfWork.Employee.GetAllAsync()).Count;
			if (totalRecord >= 0)
			{
				return new ServiceResult()
				{
					Success = true,
					Code = System.Net.HttpStatusCode.OK,
					Data = totalRecord,
					UserMsg = Resources.MsgResource_VN.GetSuccess,
					MsgResource_VN = Resources.MsgResource_VN.GetSuccess,
				};
			}
			else
			{
				throw new ValidateExeption(Resources.MsgResource_VN.GetErr);
			}
		}

	}
}

