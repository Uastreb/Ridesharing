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
    public class TripRepository : ITripRepository
    {
        private readonly string connectionString;

        public TripRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Trip>> GetAll()
        {
            IEnumerable<Trip> trips;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                trips = await db.QueryAsync<Trip>("SELECT * FROM Trips");
            }
            return trips;
        }

        public async Task<Trip> Get(int id)
        {
            Trip trip = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                trip = await db.QueryFirstOrDefaultAsync<Trip>("SELECT * FROM Trips WHERE id = @id", new { id });
            }
            return trip;
        }

        public async Task<int?> Create(Trip trip)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Trips (NumberOfSeats, Status, RegistrationEndDate, CarID, DriverID) " +
                    "VALUES(@NumberOfSeats, @Status, @RegistrationEndDate, @CarID, @DriverID); " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? tripId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, trip);
               return tripId;
            }
        }

        public async Task<int?> Update(Trip trip)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Trips SET NumberOfSeats = @NumberOfSeats, Status = @Status, " +
                    "RegistrationEndDate = @RegistrationEndDate, CarID = @CarID, DriverID = @DriverID WHERE id = @id";
                var tripId = await db.ExecuteAsync(sqlQuery, trip);
                return tripId;
            }
        }

        public async Task<bool?> Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Trips WHERE id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
                return true;
            }
        }
    }
}
