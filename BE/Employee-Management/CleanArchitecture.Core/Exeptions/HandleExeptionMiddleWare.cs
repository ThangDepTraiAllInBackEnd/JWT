using CleanArchitecture.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Exeptions
{

	public class HandleExeptionMiddleWare
	{
		private RequestDelegate _next;
		public HandleExeptionMiddleWare(RequestDelegate next)
		{
			_next = next;
		}

		/// <summary>
		/// Catch all exeption by from client ( and from dev ) 
		/// </summary>
		/// <param name="context">Current context </param>
		/// <returns>JSON response with details exeption</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created_at: 2023/12/2
		public async Task Invoke(HttpContext context)
		{
			context.Response.Headers.Add("Content-Type", "application/json");
			try
			{
				await _next(context);
			}
			catch (BadRequestCustomException ex)
			{
				ServiceResult serviceResult = new ServiceResult()
				{
					Success = false,
					Code = System.Net.HttpStatusCode.BadRequest,
					Data = null,
					DevMsg = $"{ex.Message}",
					UserMsg = $"{ex.Message}",
				};
				var res = JsonConvert.SerializeObject(serviceResult);
				await context.Response.WriteAsync(res);
			}
			catch(NotFoundCustomException ex) {
                ServiceResult serviceResult = new ServiceResult()
                {
                    Success = false,
                    Code = System.Net.HttpStatusCode.NotFound,
                    Data = null,
                    DevMsg = $"{ex.Message}",
                    UserMsg = $"{ex.Message}",
                };
                var res = JsonConvert.SerializeObject(serviceResult);
                await context.Response.WriteAsync(res);
            }
			catch(InternalServerErrorCustomException ex)
			{
                ServiceResult serviceResult = new ServiceResult()
                {
                    Success = false,
                    Code = System.Net.HttpStatusCode.InternalServerError,
                    Data = null,
                    DevMsg = $"{ex.Message}",
                    UserMsg = $"{ex.Message}",
                };
                var res = JsonConvert.SerializeObject(serviceResult);
                await context.Response.WriteAsync(res);
            }
			catch (Exception ex)
			{
				ServiceResult serviceResult = new ServiceResult()
				{
					Success = false,
					Code = System.Net.HttpStatusCode.InternalServerError,
					Data = null,
					DevMsg = $"{ex.Message}",
					UserMsg = $"{ex.Message}",
				};
				var res = JsonConvert.SerializeObject(serviceResult);
				await context.Response.WriteAsync(res);
			}
		}
	}
}
