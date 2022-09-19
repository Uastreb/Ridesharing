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
    public class DriverRepository : IDriverRepository
    {
        private readonly string connectionString;

        public DriverRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            IEnumerable<Driver> drivers;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                drivers = await db.QueryAsync<Driver>("SELECT * FROM Drivers");
            }
            return drivers;
        }

        public async Task<Driver> Get(int id)
        {
            Driver driver = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                driver = await db.QueryFirstOrDefaultAsync<Driver>("SELECT * FROM Drivers WHERE id = @id", new { id });
            }
            return driver;
        }

        public async Task<int?> Create(Driver driver)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Drivers (DateOfRegistration, Name, Surname, Patronymic, " +
                    "DateOfBirth, Telephone, Comments, Enabled, ReasonForBlocking, AccountInformationID) " +
                    "VALUES(@DateOfRegistration, @Name, @Surname, @Patronymic, @DateOfBirth," +
                    " @Telephone, @Comments, @Enabled, @ReasonForBlocking, @AccountInformationID); " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? driverId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, driver);
                return driverId;
            }
        }

        public async Task<int?> Update(Driver driver)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Drivers SET DateOfRegistration = @DateOfRegistration, Name = @Name," +
                    " Surname = @Surname, Patronymic = @Patronymic, DateOfBirth = @DateOfBirth, Telephone = @Telephone," +
                    "Comments = @Comments, Enabled = @Enabled, ReasonForBlocking = @ReasonForBlocking, " +
                    "AccountInformationID = @AccountInformationID WHERE id = @id";
                var driverId = await db.ExecuteAsync(sqlQuery, driver);
                return driverId;
            }
        }

        public async Task<bool?> Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Drivers WHERE id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
                return true;
            }
        } 

        public async Task<Driver> FindAccount(int AccountInformationId)
        {
            Driver driver = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                driver = await db.QueryFirstOrDefaultAsync<Driver>("SELECT * FROM Drivers WHERE AccountInformationID = @AccountInformationID", new { AccountInformationId });
            }
            return driver;
        }

    }
}
