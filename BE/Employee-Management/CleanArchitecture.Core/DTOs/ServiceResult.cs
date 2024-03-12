﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs
{
	public class ServiceResult
	{
		/// <summary>
		/// API status
		/// </summary>
		public bool Success { get; set; }
		/// <summary>
		/// API data 
		/// </summary>
		public object Data { get; set; }
		/// <summary>
		/// api status Code
		/// </summary>
		public HttpStatusCode Code { get; set; }
		/// <summary>
		/// Developer message
		/// </summary>
		public string MsgResource_VN { get; set; }
		/// <summary>
		/// user message
		/// </summary>
		public string UserMsg { get; set; }
	}
}
