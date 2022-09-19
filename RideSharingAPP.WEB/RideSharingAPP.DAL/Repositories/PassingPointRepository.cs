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
    public class PassingPointRepository : IPassingPointRepository
    {
        private readonly string connectionString;

        public PassingPointRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<PassingPoint>> GetAll()
        {
            IEnumerable<PassingPoint> passingPoints;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                passingPoints = await db.QueryAsync<PassingPoint>("SELECT * FROM PassingPoints");
            }
            return passingPoints;
        }

        public async Task<PassingPoint> Get(int id)
        {
            PassingPoint passingPoint = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                passingPoint = await db.QueryFirstOrDefaultAsync<PassingPoint>("SELECT * FROM PassingPoints WHERE id = @id", new { id });
            }
            return passingPoint;
        }

        public async Task<int?> Create(PassingPoint passingPoint)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO PassingPoints (OriginCoordinates, EndCoordinates, DateAndTimeOfDepartue, " +
                    "DateAndTimeOfArrival, Cost, PointNumberOfThisTrip, TripID) VALUES(" +
                    "@OriginCoordinates, @EndCoordinates, @DateAndTimeOfDepartue, @DateAndTimeOfArrival, " +
                    "@Cost, @PointNumberOfThisTrip, @TripID); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? passingPointId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, passingPoint);
                return passingPointId;
            }
        }

        public async Task<int?> Update(PassingPoint passingPoint)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE PassingPoints SET OriginCoordinates = @OriginCoordinates, EndCoordinates = @EndCoordinates, " +
                    "DateAndTimeOfDepartue = @DateAndTimeOfDepartue, DateAndTimeOfArrival = @DateAndTimeOfArrival, " +
                    "Cost = @Cost, PointNumberOfThisTrip = @PointNumberOfThisTrip, TripID = @TripID" +
                    " WHERE id = @id";
                var passingPointId = await db.ExecuteAsync(sqlQuery, passingPoint);
                return passingPointId;
            }
        }

        public async Task<bool?> Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM PassingPoints WHERE id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
                return true;
            }
        } 
    }
}
