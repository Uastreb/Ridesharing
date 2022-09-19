using RideSharingAPP.BLL.DTO;
using RideSharingAPP.BLL.DTO.CarDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces
{
    public interface ICarService : IDisposable
    {
        Task<IEnumerable<CarDTOCreate>> FindCars(int driverId);
        Task<int?> Create(CarDTOCreate car);
        Task<IEnumerable<CarDTOCreate>> GetAll();
        Task<IEnumerable<CarDTOGetCars>> GetAllForDropDown(int driverId);
        Task<CarDTOCreate> Get(int CarId);
        Task<int?> EditCar(CarDTOCreate car);
        Task<bool?> DeleteCar(int CarId);
    }
}
