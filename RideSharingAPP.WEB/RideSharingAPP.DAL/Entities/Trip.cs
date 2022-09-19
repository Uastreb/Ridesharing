using System;

namespace RideSharingApp.DAL.Entities
{
    public class Trip
    {
        public int id { get; set; }
        public int NumberOfSeats { get; set; }
        public int Status { get; set; }
        public DateTime RegistrationEndDate { get; set; }

        public int CarId { get; set; }
        public int DriverId { get; set; }
    }
}
