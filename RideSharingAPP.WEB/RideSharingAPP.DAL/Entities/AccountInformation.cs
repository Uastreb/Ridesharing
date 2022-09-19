namespace RideSharingApp.DAL.Entities
{
    public class AccountInformation
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DynamicSalt { get; set; }
    }
}
