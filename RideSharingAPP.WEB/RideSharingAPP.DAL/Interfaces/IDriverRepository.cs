using RideSharingApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RideSharingApp.DAL.Interfaces
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver> FindAccount(int AccountInformationId);
    }
}
