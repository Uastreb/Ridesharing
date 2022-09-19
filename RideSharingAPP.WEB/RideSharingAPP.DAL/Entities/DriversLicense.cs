using System;

namespace RideSharingApp.DAL.Entities
{
    public class DriversLicense
    {
        public int id { get; set; }
        public DateTime DateOfReceiving { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DriverLicensesNumber { get; set; }
        public string IssuingAuthority { get; set; }
        public int Categories { get; set; }

        public int DriverID { get; set; }
    }
}
