using RideSharingApp.BLL.Services;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.ClientDTO;
using RideSharingAPP.BLL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services
{
    public class ClientService : IClientService
    {
        IUnitOfWork Database { get; set; }

        public ClientService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<int?> Create(ClientDTOCreate client)
        {
            var mappedClient = new AutoMap<Client, ClientDTOCreate>().Initialize(client);
            mappedClient.DateOfRegistration = DateTime.Now;
            int? newClientsId = await Database.Clients.Create(mappedClient);
            Database.Commit();
            return newClientsId;
        }

        public async Task<ClientDTOCreate> Get(int id)
        {
            var client = await Database.Clients.Get(id);
            var mappedCLient = new AutoMap<ClientDTOCreate, Client>().Initialize(client);
            return mappedCLient;
        }

        public async Task<ClientDTOCreate> FindAccount(int accountInformationId)
        {
            var accounts = await Database.Clients.GetAll();
            var client = accounts.FirstOrDefault(x => x.AccountInformationID == accountInformationId);
            return new AutoMap<ClientDTOCreate, Client>().Initialize(client);
        }

        public async Task<int?> Update(ClientDTOCreate client)
        {
            var mappedClient = new AutoMap<Client, ClientDTOCreate>().Initialize(client);
            var checkResult = await Database.Clients.Update(mappedClient);
            Database.Commit();
            return checkResult;
        }

        public async Task<ClientDTOCreate> FindPassenger(int accountInformationId)
        {
            var client = await Database.Clients.FindPassenger(accountInformationId);
            return new AutoMap<ClientDTOCreate, Client>().Initialize(client);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
