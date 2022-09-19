using RideSharingApp.DAL.Entities;
using System.Data.Entity;


namespace RideSharingApp.DAL.EF
{
    public class RideSharingContext : DbContext
    {
		public DbSet <Driver> Drivers { get; set; }
		public DbSet <Car> Cars { get; set; }
		public DbSet <DriversLicense> DriversLicenses { get; set; }
		public DbSet <Trip> Trips { get; set; }
		public DbSet <Client> Clients { get; set; }
		public DbSet <AccountInformation> AccountsInformation { get; set; }
		public DbSet <Companion> Companions { get; set; }
		public DbSet <PassingPoint> PassingPoints { get; set; }

        static RideSharingContext()
        {
            Database.SetInitializer<RideSharingContext>(new DropCreateDatabaseAlways<RideSharingContext>());
        }

        public RideSharingContext(string connectionString)
            : base(connectionString)
        {
        }
    }


    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<RideSharingContext>
    {
        protected override void Seed(RideSharingContext db)
        {
        }
    }
}
