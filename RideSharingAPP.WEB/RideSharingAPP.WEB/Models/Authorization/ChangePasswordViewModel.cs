using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Authorization
{
    public class ChangePasswordViewModel
    {
        /// <summary>Field storing user password.</summary>
        [Display(Name = "Password"), Required(ErrorMessage = "Не указан пароль пользователя"),
            StringLength(50, MinimumLength = 6, ErrorMessage = "Недопустимая длина пароля")]
        public string Password { get; set; }
    }
}