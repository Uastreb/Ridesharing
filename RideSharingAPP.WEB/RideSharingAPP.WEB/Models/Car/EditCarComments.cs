using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Car
{
    public class EditCarComments
    {
        [StringLength(maximumLength: 100, ErrorMessage = "Недопустимая длина")]
        [Display(Name = "Комментарий")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public int id { get; set; }

        [ScaffoldColumn(false)]
        public string Mark { get; set; }

        [ScaffoldColumn(false)]
        public string Model { get; set; }

        [ScaffoldColumn(false)]
        public int YearOfIssue { get; set; }

        [ScaffoldColumn(false)]
        public string RegistrationNumber { get; set; }

        [ScaffoldColumn(false)]
        public int DriverId { get; set; }

        [ScaffoldColumn(false)]
        public bool Deleted { get; set; }
    }
}