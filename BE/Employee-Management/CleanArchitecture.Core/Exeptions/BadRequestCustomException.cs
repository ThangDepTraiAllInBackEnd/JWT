using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Exeptions
{
	public class BadRequestCustomException : Exception
	{
		private string MsgError = string.Empty;
		public BadRequestCustomException(string error)
		{
			this.MsgError = error;
		}
		public override string Message => this.MsgError;
	}
}
