using RideSharingApp.BLL.Services;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.DriverDTO;
using RideSharingAPP.BLL.Interfaces;
using System;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services
{
    public class DriverService : IDriverService
    {
        IUnitOfWork Database { get; set; }

        public DriverService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<DriverDTOCreate> FindAccount(int accountInformationId)
        {
            var driver = await Database.Drivers.FindAccount(accountInformationId);
            return new AutoMap<DriverDTOCreate, Driver>().Initialize(driver);
        }

        public async Task<int?> Update(DriverDTOCreate driver)
        {
            var mappedDriver = new AutoMap<Driver, DriverDTOCreate>().Initialize(driver);
            var checkResult = await Database.Drivers.Update(mappedDriver);
            Database.Commit();
            return checkResult;
        }

        public async Task<DriverDTOCreate> Get(int id)
        {
            var driver = await Database.Drivers.Get(id);
            var mappedDriver = new AutoMap<DriverDTOCreate, Driver>().Initialize(driver);
            return mappedDriver;
        }

        public async Task<int?> Create(DriverDTOCreate driver)
        {
            var mappingDriver = new AutoMap<Driver, DriverDTOCreate>().Initialize(driver);
            mappingDriver.DateOfRegistration = DateTime.Now;
            int? newDriversAccountId = await Database.Drivers.Create(mappingDriver);
            Database.Commit();
            return newDriversAccountId;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
