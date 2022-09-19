using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RideSharingAPP.WEB.Models.Driver
{
    public class TripViewModel
    {
        public int id { get; set; }

        public int NumberOfSeats { get; set; }
        public DateTime RegistrationEndDate { get; set; }

        public SelectList ListCars { get; set; }
        public int CarId { get; set; }
        public int DriverId { get; set; }
        public IEnumerable<PassingPointViewModel> Points { get; set; }
    }
}