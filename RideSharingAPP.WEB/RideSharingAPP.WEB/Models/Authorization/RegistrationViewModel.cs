using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RideSharingAPP.WEB.Models.Authorization
{
    public class RegistrationViewModel
    {
        [Display(Name = "Email"), Required(ErrorMessage = "Не указан электронный адрес"),
            RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный электронный адрес"),
            Remote(action: "IsIndividualEmail", controller: "Authorization", HttpMethod = "POST", ErrorMessage = "Email уже используется")]
        public string Email { get; set; }

        [Display(Name = "Password"), Required(ErrorMessage = "Не указан пароль пользователя"),
            StringLength(50, MinimumLength = 6, ErrorMessage = "Недопустимая длина пароля")]
        public string Password { get; set; } 
    }
}