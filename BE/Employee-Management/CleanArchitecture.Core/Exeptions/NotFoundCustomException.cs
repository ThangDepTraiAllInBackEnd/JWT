using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Exeptions
{
    public class NotFoundCustomException : Exception
    {
        private string MsgError = string.Empty;
        public NotFoundCustomException(string error)
        {
            this.MsgError = error;
        }
        public override string Message => this.MsgError;
    }
}
