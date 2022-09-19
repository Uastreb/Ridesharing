using RideSharingApp.DAL.EF;
using RideSharingApp.DAL.Interfaces;
using RideSharingApp.DAL.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RideSharingApp.DAL.Repositories
{
    public class AccountInformationRepository : IAccountInformationRepository
    {
        private readonly string connectionString;

        public AccountInformationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<AccountInformation>> GetAll()
        {
            IEnumerable<AccountInformation> accounts;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                accounts = await db.QueryAsync<AccountInformation>("SELECT * FROM AccountsInformation");
            }
            return accounts;
        }

        public async Task<AccountInformation> Get(int id)
        {
            AccountInformation account = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                account = await db.QueryFirstOrDefaultAsync<AccountInformation>("SELECT * FROM AccountsInformation WHERE id = @id", new { id });
            }
            return account;
        }

        public async Task<int?> Create(AccountInformation account)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO AccountsInformation (Email, Password, DynamicSalt) VALUES(@Email, @Password, @DynamicSalt); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? acountId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, account);
                return acountId;
            }
        }

        public async Task<int?> Update(AccountInformation account)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE AccountsInformation SET Password = @Password, DynamicSalt = @DynamicSalt, Email = @Email WHERE id = @id; " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                var editAccountInformationId = await db.ExecuteAsync(sqlQuery, account);
                return editAccountInformationId;
            }
        }

        public async Task<bool?> Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM AccountsInformation WHERE id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
                return true;
            }
        }

        public async Task<int?> ChangePassword(string password, int accountInformationId, string dynamicSalt)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE AccountsInformation SET Password = @password, DynamicSalt = @dynamicSalt WHERE id = @accountInformationId; " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                var editAccountInformationId = await db.ExecuteAsync(sqlQuery, new { password, accountInformationId, dynamicSalt });
                return editAccountInformationId;
            }
        }

        public async Task<AccountInformation> FindEmail(string email)
        {
            AccountInformation account;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                account = await db.QueryFirstOrDefaultAsync<AccountInformation>("SELECT * FROM AccountsInformation WHERE Email = @Email", new { email });
            }
            return account;
        }
    }
}
