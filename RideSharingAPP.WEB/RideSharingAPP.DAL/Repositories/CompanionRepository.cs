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
    public class CompanionRepository : ICompanionRepository
    {
        private readonly string connectionString;

        public CompanionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Companion>> GetAll()
        {
            IEnumerable<Companion> companions;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                companions = await db.QueryAsync<Companion>("SELECT * FROM Companions");
            }
            return companions;
        }

        public async Task<Companion> Get(int id)
        {
            Companion companion = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                companion = await db.QueryFirstOrDefaultAsync<Companion>("SELECT * FROM Companions WHERE id = @id", new { id });
            }
            return companion;
        }

        public async Task<int?> Create(Companion companion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Companions (ClientID, TripID, TotalCost, DateAndTimeOfDepartue, " +
                    "DateAndTimeOfArrival, OriginCoordinates, EndCoordinates) VALUES(@ClientID, " +
                    "@TripID, @TotalCost, @DateAndTimeOfDepartue, @DateAndTimeOfArrival, @OriginCoordinates," +
                    "@EndCoordinates); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? companionId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, companion);
                return companionId;
            }
        }

        public async Task<int?> Update(Companion companion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Companions SET ClientID = @ClientID, TripID = @TripID" +
                    "TotalCost = @TotalCost, DateAndTimeOfDepartue = @DateAndTimeOfDepartue, DateAndTimeOfArrival =" +
                    "@DateAndTimeOfArrival, OriginCoordinates = @OriginCoordinates, EndCoordinates = " +
                    "@EndCoordinates WHERE id = @id";
                var companionId = await db.ExecuteAsync(sqlQuery, companion);
                return companionId;
            }
        }

        public async Task<bool?> Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Companions WHERE id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
                return true;
            }

        }

        public async Task<IEnumerable<Companion>> Find(string predicate)
        {
            IEnumerable<Companion> companions;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT * FROM Companions WHERE @predicate";
                companions = await db.QueryAsync<Companion>(sqlQuery, new { predicate });
            }
            return companions;
        }
    }
}
