namespace RideSharingApp.DAL.Entities
{
    public class Car
    {
        public int id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public int YearOfIssue { get; set; }
        public string RegistrationNumber { get; set; }
        public string Comments { get; set; }
        public bool Deleted { get; set; }

        public int DriverID { get; set; }
    }
}
