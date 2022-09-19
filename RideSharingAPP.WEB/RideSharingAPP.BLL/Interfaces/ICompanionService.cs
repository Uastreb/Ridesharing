using RideSharingAPP.BLL.DTO.ClientDTO;
using RideSharingAPP.BLL.DTO.CompanionDTO;
using RideSharingAPP.BLL.DTO.TripDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces
{
    public interface ICompanionService : IDisposable
    {
        Task<bool?> DeletedForTheRoute(int id);
        Task<int?> RegistrationForTheRoute(CompanionDTOCreate companion);
        Task<CompanionDTOCreate> Get(int companionId);
        Task<IEnumerable<CompanionDTOCreate>> GetAll();
        Task<IEnumerable<CompanionDTOCreate>> GetActiveUserRoutes(int clientId);
        Task<IEnumerable<ClientDTOwithCompanionDate>> GetClients(int tripId);
        Task<string> GetDriverTelephone(int tripId);
        Task<bool?> Unregister(int companionId);
    }
}
