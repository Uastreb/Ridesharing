using RideSharingApp.DAL.EF;
using RideSharingApp.DAL.Interfaces;
using RideSharingApp.DAL.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace RideSharingApp.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly string connectionString;

        public CarRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            IEnumerable<Car> cars;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                cars = await db.QueryAsync<Car>("SELECT * FROM Cars");
            }
            return cars;
        }

        public async Task<Car> Get(int id)
        {
            Car car = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                car = await db.QueryFirstOrDefaultAsync<Car>("SELECT * FROM Cars WHERE id = @id", new { id });
            }
            return car;
        }

        public async Task<int?> Create(Car car)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Cars (Mark, Model, YearOfIssue, RegistrationNumber," +
                    " Comments, Deleted, DriverID) VALUES(@Mark, @Model, @YearOfIssue, @RegistrationNumber," +
                    " @Comments, @Deleted, @DriverID); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? carId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, car);
                return carId;
            }
        }

        public async Task<int?> Update(Car car)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Cars SET Mark = @Mark, Model = @Model, YearOfIssue = @YearOfIssue, " +
                    "RegistrationNumber = @RegistrationNumber, Comments = @Comments, Deleted = @Deleted, " +
                    "DriverID = @DriverID WHERE id = @id; SELECT CAST(SCOPE_IDENTITY() as int)";
               int carId = await db.ExecuteAsync(sqlQuery, car);
                return carId;
            }
        }

        public async Task<bool?> Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Cars WHERE id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
                return true;
            }
        }

        public async Task<IEnumerable<Car>> FindCars(int DriverId)
        {
            IEnumerable<Car> cars;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                cars = await db.QueryAsync<Car>("SELECT * FROM Cars Where DriverID = @DriverID ", new { DriverId });
            }
            return cars;
        }
    }
}
