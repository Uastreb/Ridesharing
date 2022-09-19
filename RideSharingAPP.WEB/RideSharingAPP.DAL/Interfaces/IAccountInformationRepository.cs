using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RideSharingApp.DAL.Entities;
using static Dapper.SqlMapper;

namespace RideSharingApp.DAL.Interfaces
{
    public interface IAccountInformationRepository : IRepository<AccountInformation>
    {
        Task<AccountInformation> FindEmail(string email);
        Task<int?> ChangePassword(string password, int accountInformationId, string dynamicSalt);
    }
}
