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
    public class DriversLicenseRepository : IDriverLicensesRepository
    {
        private readonly string connectionString;

        public DriversLicenseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<DriversLicense>> GetAll()
        {
            IEnumerable<DriversLicense> driversLicenses;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                driversLicenses = await db.QueryAsync<DriversLicense>("SELECT * FROM DriversLicenses");
            }
            return driversLicenses;
        }

        public async Task<DriversLicense> Get(int id)
        {
            DriversLicense driversLicense = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                driversLicense = await db.QueryFirstOrDefaultAsync<DriversLicense>("SELECT * FROM DriversLicenses WHERE id = @id", new { id });
            }
            return driversLicense;
        }

        public async Task<int?> Create(DriversLicense driversLicense)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO DriversLicenses (DateOfReceiving, ExpirationDate, DriverLicensesNumber, " +
                    "IssuingAuthority, Categories, DriverID) VALUES(@DateOfReceiving, @ExpirationDate, " +
                    "@DriverLicensesNumber, @IssuingAuthority, @Categories, @DriverID); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? driversLicense_Id = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, driversLicense);
                return driversLicense_Id;
            }
        }

        public async Task<int?> Update(DriversLicense driversLicense)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE DriversLicenses SET DateOfReceiving = @DateOfReceiving, ExpirationDate = @ExpirationDate, " +
                    "DriverLicensesNumber = @DriverLicensesNumber, IssuingAuthority = @IssuingAuthority, " +
                    "Categories = @Categories, DriverID = @DriverID WHERE id = @id";
                var driverLicensesId = await db.ExecuteAsync(sqlQuery, driversLicense);
                return driverLicensesId;
            }
        }

        public async Task<bool?> Delete(int id)
        {

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM DriversLicenses WHERE id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
                return true;
            }
        }

        public async Task<DriversLicense> FindDriverLicenses(int DriverId)
        {
            DriversLicense driverLicenses = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                driverLicenses = await db.QueryFirstOrDefaultAsync<DriversLicense>("SELECT * FROM DriversLicenses WHERE DriverID = @DriverID", new { DriverId });
            }
            return driverLicenses;
        }
    }
}
