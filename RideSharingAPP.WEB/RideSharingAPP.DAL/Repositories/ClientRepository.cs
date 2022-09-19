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
    public class ClientRepository : IClientRepository
    {
        private readonly string connectionString;

        public ClientRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            IEnumerable<Client> clients;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                clients = await db.QueryAsync<Client>("SELECT * FROM Clients");
            }
            return clients;
        }

        public async Task<Client> Get(int id)
        {
            Client client = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                client = await db.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Clients WHERE id = @id", new { id });
            }
            return client;
        }

        public async Task<int?> Create(Client client)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Clients (DateOfRegistration, Name, Surname, Patronymic, " +
                    "DateOfBirth, Telephone, Comments, Enabled, ReasonForBlocking, AccountInformationID) " +
                    "VALUES(@DateOfRegistration, @Name, @Surname, @Patronymic, @DateOfBirth," +
                    " @Telephone, @Comments, @Enabled, @ReasonForBlocking, @AccountInformationID); " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? clientId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, client);
                return clientId;
            }
        }

        public async Task<int?> Update(Client client)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Clients SET DateOfRegistration = @DateOfRegistration, Name = @Name," +
                    " Surname = @Surname, Patronymic = @Patronymic, DateOfBirth = @DateOfBirth, Telephone = @Telephone, " +
                    "Comments = @Comments, Enabled = @Enabled, ReasonForBlocking = @ReasonForBlocking, " +
                    "AccountInformationID = @AccountInformationID WHERE id = @id";
                var clietnId = await db.ExecuteAsync(sqlQuery, client);
                return clietnId;
            }
        }

        public async Task<bool?> Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Clients WHERE id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
                return true;
            }
        }

        public async Task<Client> FindPassenger(int AccountInformationID)
        {
            Client passenger = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                passenger = await db.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Clients WHERE AccountInformationID = @AccountInformationID", new { AccountInformationID });
            }
            return passenger;
        }
    }
}
