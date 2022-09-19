using System;

namespace RideSharingAPP.WEB.Models.Driver
{
    public class PassingPointViewModel
    {
        public int id { get; set; }

        public string OriginCoordinates { get; set; }
        public string EndCoordinates { get; set; }
        public DateTime DateAndTimeOfDepartue { get; set; }
        public DateTime DateAndTimeOfArrival { get; set; }
        public decimal Cost { get; set; }

        public int TripID { get; set; }
    }
}