using RideSharingAPP.BLL.DTO;
using RideSharingAPP.BLL.DTO.ClientDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces
{
    public interface IClientService : IDisposable
    {
        Task<ClientDTOCreate> FindPassenger(int accountInformationId);
        Task<int?> Create(ClientDTOCreate passenger);
        Task<int?> Update(ClientDTOCreate client);
        Task<ClientDTOCreate> FindAccount(int accountInformationId);
        Task<ClientDTOCreate> Get(int id);
    }
}
