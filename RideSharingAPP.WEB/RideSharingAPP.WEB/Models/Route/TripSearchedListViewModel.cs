using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Route
{
    public class TripSearchedListViewModel
    {
        public int id { get; set; }
        public int NumberOfSeats { get; set; }
        public int Status { get; set; }
        public DateTime RegistrationEndDate { get; set; }

        public string OriginCoordinates { get; set; }
        public string EndCoordinates { get; set; }
        public DateTime DateAndTimeOfDepartue { get; set; }
        public DateTime DateAndTimeOfArrival { get; set; }
        public decimal TotalСost { get; set; }

        public IEnumerable<PassingPoint.PassingPointForSearhedListViewModel> PassingPoints { get; set; }
        public int CarId { get; set; }
        public Car.CarViewModel Car { get; set; }
        public int DriverId { get; set; }
        public Driver.DriverViewModel Driver {get;set;}
    }
}