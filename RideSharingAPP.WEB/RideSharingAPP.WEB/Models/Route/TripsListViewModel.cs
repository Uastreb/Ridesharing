using RideSharingAPP.WEB.Models.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Route
{
    public class TripsListViewModel
    {
        public int id { get; set; }

        public int NumberOfSeats { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public int Status { get; set; }

        public int CarId { get; set; }
        public Car.CarViewModel Car { get; set; }
        public IEnumerable<PassingPointViewModel> Points { get; set; }
        public IEnumerable<Client.ClientWithCompanionDateViewModel> Clients { get; set; }
    }
}