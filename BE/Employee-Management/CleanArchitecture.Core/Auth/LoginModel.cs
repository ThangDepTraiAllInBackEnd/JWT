using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = Const.AuthentionModelErrMsg.USERNAME_IS_REQURIED)]
        public string? Username { get; set; }

        [Required(ErrorMessage = Const.AuthentionModelErrMsg.PASSWORD_IS_REQURIED)]
        public string? Password { get; set; }
    }
}
