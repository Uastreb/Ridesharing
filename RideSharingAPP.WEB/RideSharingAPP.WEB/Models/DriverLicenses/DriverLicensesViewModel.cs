using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RideSharingAPP.WEB.Models.DriverLicenses
{
    public class DriverLicensesViewModel
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Дата получения")]
        public DateTime DateOfReceiving { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Срок действия")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Номер водительского удостоверения")]
        public string DriverLicensesNumber { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Орган выдавший удостоверение")]
        public string IssuingAuthority { get; set; }


        public SelectList ListCategories{ get; set; }


        [ScaffoldColumn(false)]
        public int idCategory { get; set; }
        [ScaffoldColumn(false)]
        public int id { get; set; }
        [ScaffoldColumn(false)]
        public int DriverID { get; set; }
    }
}