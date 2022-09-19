using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Car
{
    public class DeleteCar
    {
        [ScaffoldColumn(false)]
        public bool Deleted { get; set; }
    }
}