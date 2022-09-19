using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Companion
{
    public class CompanionViewModel
    {
        public int id { get; set; }

        public int ClientID { get; set; }

        public int TripID { get; set; }

        public string Telephone { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime DateAndTimeOfDepartue { get; set; }
        public DateTime DateAndTimeOfArrival { get; set; }
        public string OriginCoordinates { get; set; }
        public string EndCoordinates { get; set; }
    }
}