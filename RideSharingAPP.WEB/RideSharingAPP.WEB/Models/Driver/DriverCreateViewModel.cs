using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Driver
{
    public class DriverCreateViewModel
    {
        [ScaffoldColumn(false)]
        public int id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Номер телефона")]
        public string Telephone { get; set; }


        [Display(Name = "Немного о себе")]
        public string Comments { get; set; }


        [ScaffoldColumn(false)]
        public DateTime DateOfRegistration { get; set; }
        [ScaffoldColumn(false)]
        public bool Enabled { get; set; }
        [ScaffoldColumn(false)]
        public string ReasonForBlocking { get; set; }
        [ScaffoldColumn(false)]
        public int AccountInformationID { get; set; }
    }
}