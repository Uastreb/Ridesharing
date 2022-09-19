using RideSharingApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace RideSharingApp.DAL.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> FindPassenger(int AccountInformationID);
    }
}
