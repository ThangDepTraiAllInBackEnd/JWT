using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = Const.AuthentionModelErrMsg.USERNAME_IS_REQURIED)]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = Const.AuthentionModelErrMsg.EMAIL_IS_NOTVALID)]
        [Required(ErrorMessage = Const.AuthentionModelErrMsg.EMAIL_IS_REQURIED)]
        public string? Email { get; set; }

        [Required(ErrorMessage = Const.AuthentionModelErrMsg.PASSWORD_IS_REQURIED)]
        public string? Password { get; set; }
    }
}
