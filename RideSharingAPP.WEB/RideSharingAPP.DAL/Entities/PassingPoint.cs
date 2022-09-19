using System;

namespace RideSharingApp.DAL.Entities
{
    public class PassingPoint
    {
        public int id { get; set; }
        public string OriginCoordinates { get; set; }
        public string EndCoordinates { get; set; }
        public DateTime DateAndTimeOfDepartue { get; set; }
        public DateTime DateAndTimeOfArrival { get; set; }
        public decimal Cost { get; set; }
        public int PointNumberOfThisTrip { get; set; }

        public int TripID { get; set; }
    }
}
