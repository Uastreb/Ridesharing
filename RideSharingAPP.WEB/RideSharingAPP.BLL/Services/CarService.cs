using RideSharingApp.BLL.Services;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.CarDTO;
using RideSharingAPP.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services
{
    public class CarService : ICarService
    {
        IUnitOfWork Database { get; set; }

        public CarService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<int?> Create(CarDTOCreate car)
        {
            var mappedCar = new AutoMap<Car, CarDTOCreate>().Initialize(car);
            int? newCarId = await Database.Cars.Create(mappedCar);
            Database.Commit();
            return newCarId;
        }

        public async Task<int?> EditCar(CarDTOCreate car)
        {
            var mappedCar = new AutoMap<Car, CarDTOCreate>().Initialize(car);
            int? editsCarId = await Database.Cars.Update(mappedCar);
            Database.Commit();
            return editsCarId;
        }

        public async Task<IEnumerable<CarDTOCreate>> FindCars(int driverId)
        {
            var cars = await Database.Cars.FindCars(driverId);
            var mappedCars = cars.Select(p => new AutoMap<CarDTOCreate, Car>().Initialize(p)).ToList();
            return mappedCars;
        }

        public async Task<bool?> DeleteCar(int carId)
        {
            var result = await Database.Cars.Delete(carId);
            Database.Commit();
            return result;
        }

        public async Task<IEnumerable<CarDTOCreate>> GetAll()
        {
            var cars = await Database.Cars.GetAll();
            var mappedCars = cars.Select(p => new AutoMap<CarDTOCreate, Car>().Initialize(p)).ToList();
            return mappedCars;
        }

        public async Task<CarDTOCreate> Get(int carId)
        {
            var car = await Database.Cars.Get(carId);
            var mappedCar = new AutoMap<CarDTOCreate, Car>().Initialize(car);
            return mappedCar;
        }

        public async Task<IEnumerable<CarDTOGetCars>> GetAllForDropDown(int driverId)
        {
            var cars = await Database.Cars.GetAll();
            cars = cars.Where(x => x.DriverID == driverId);
            var mappedCars = cars.Select(p => new AutoMap<CarDTOGetCars, Car>().Initialize(p)).ToList();
            return mappedCars;
        }
    }
}
