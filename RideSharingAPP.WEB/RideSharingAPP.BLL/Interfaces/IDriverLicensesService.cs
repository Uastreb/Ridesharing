using RideSharingAPP.BLL.DTO;
using RideSharingAPP.BLL.DTO.DriverLicensesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces
{
    public interface IDriverLicensesService : IDisposable
    {
        Task<int?> Create(DriverLicensesDTOCreate driverLicenses);
        Task<DriverLicensesDTOCreate> FindDriverLicenses(int driverId);
    }
}
