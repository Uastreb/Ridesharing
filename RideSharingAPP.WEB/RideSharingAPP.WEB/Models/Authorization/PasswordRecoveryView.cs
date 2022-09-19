using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RideSharingAPP.WEB.Models.Authorization
{
    public class PasswordRecoveryView
    {
        [Display(Name = "Email"), Required(ErrorMessage = "Не указан электронный адрес"),
            RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный электронный адрес")]
        public string Email { get; set; }
    }
}