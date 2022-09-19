using System.ComponentModel.DataAnnotations;

namespace RideSharingAPP.WEB.Models.Authorization
{
    /// <summary>Model storing data from a user account.</summary>
    public class InputViewModel
    {
        /// <summary>Field storing user email.</summary>
        [Display(Name = "Email"), Required(ErrorMessage = "Не указан электронный адрес"),
            RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный электронный адрес"),
            StringLength(maximumLength: 50, MinimumLength = 4, ErrorMessage = "Недопустимая длина")]
        public string Email { get; set; }

        /// <summary>Field storing user password.</summary>
        [Display(Name = "Password"), Required(ErrorMessage = "Не указан пароль пользователя")]
        public string Password { get; set; }
    }
}