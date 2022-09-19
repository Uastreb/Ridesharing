using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Car
{
    public class CarViewModel
    {
        [ScaffoldColumn(false)]
        public int id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Марка")]
        public string Mark { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Модель")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Год выпуска")]
        public int YearOfIssue { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Регистрационный номер")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Комментарий")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public bool Deleted { get; set; }
    }
}