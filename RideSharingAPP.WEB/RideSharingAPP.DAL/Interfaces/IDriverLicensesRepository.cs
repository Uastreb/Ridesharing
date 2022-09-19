using RideSharingApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RideSharingApp.DAL.Interfaces
{
    public interface IDriverLicensesRepository : IRepository<DriversLicense>
    {
        Task<DriversLicense> FindDriverLicenses(int DriverId);
    }
}
