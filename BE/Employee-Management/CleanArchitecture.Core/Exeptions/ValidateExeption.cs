using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Exeptions
{
	public class ValidateExeption : Exception
	{
		private string MsgError = string.Empty;
		public ValidateExeption(string error)
		{
			this.MsgError = error;
		}
		public override string Message => this.MsgError;
	}
}
