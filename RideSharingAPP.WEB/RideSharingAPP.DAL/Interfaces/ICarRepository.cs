using RideSharingApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RideSharingApp.DAL.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> FindCars(int DriverId);
    }
}
