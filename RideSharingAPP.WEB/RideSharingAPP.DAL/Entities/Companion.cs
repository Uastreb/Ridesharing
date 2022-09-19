using System;

namespace RideSharingApp.DAL.Entities
{
    public class Companion
    {
        public int id { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime DateAndTimeOfDepartue { get; set; }
        public DateTime DateAndTimeOfArrival { get; set; }
        public string OriginCoordinates { get; set; }
        public string EndCoordinates { get; set; }

        public int ClientID { get; set; }

        public int TripID { get; set; }

    
    }
}
