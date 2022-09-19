using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Car
{
    public class ListCarsViewModel
    {
        [ScaffoldColumn(false)]
        public int id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Регистрационный номер")]
        public string RegistrationNumber { get; set; }

    }
}