using RideSharingAPP.BLL.DTO.DriverDTO;
using System;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces
{
    public interface IDriverService : IDisposable
    {
        Task<DriverDTOCreate> FindAccount(int accountInformationId);
        Task<int?> Create(DriverDTOCreate driver);
        Task<DriverDTOCreate> Get(int id);
        Task<int?> Update(DriverDTOCreate driver);
    }
}
