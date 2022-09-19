using RideSharingApp.BLL.Services;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.DriverLicensesDTO;
using RideSharingAPP.BLL.Interfaces;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services
{
    public class DriverLicensesService : IDriverLicensesService
    {
        IUnitOfWork Database { get; set; }

        public DriverLicensesService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<DriverLicensesDTOCreate> FindDriverLicenses(int driverId)
        {
            var driverLicenses = await Database.DriversLicenses.FindDriverLicenses(driverId);
            return new AutoMap<DriverLicensesDTOCreate, DriversLicense>().Initialize(driverLicenses);
        }

        public async Task<int?> Create(DriverLicensesDTOCreate driverLicenses)
        {
            int? newDriverLicensesId = await Database.DriversLicenses.Create(new AutoMap<DriversLicense, DriverLicensesDTOCreate>().Initialize(driverLicenses));
            Database.Commit();
            return newDriverLicensesId;
        }
    }
}
