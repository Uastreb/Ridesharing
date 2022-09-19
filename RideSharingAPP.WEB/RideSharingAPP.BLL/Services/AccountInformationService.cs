using AutoMapper;
using NLog;
using RideSharingApp.BLL.Interfaces;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.AccountInformationDTO;
using System;
using System.Threading.Tasks;

namespace RideSharingApp.BLL.Services
{
    public class AccountInformationService : IAccountInformationService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork Database { get; set; }
        // вынести в ресурсы
        readonly string staticSalt = "tko0yCAhVgc5V/ODk59hSQ==";

        public AccountInformationService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<int?> Authorization(AccountInformationDTOCreditionals account)
        {
            var acc = await Database.AccountsInformation.FindEmail(account.Email);
            if (acc!=null)
            {
                string accountPassword = "";
                for (int i = 0; i < 60; i++)
                {
                    accountPassword = MySecurityLib.Password.HashedPassword.GetHashedPassword(account.Password, staticSalt, acc.DynamicSalt, "sha256");
                }
                if (accountPassword == acc.Password)
                {
                    return acc.id;
                }
            }
            return null;
        }

        public async Task<string> FindEmail(string email)
        {
            var account = await Database.AccountsInformation.FindEmail(email);
            return account.Email;
        }
        
        // в хелпер
        private string GetRandomPassword()
        {
            int[] arr = new int[16];
            Random rnd = new Random();
            string password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                password += (char)arr[i];
            }
            return password;
        }

        public async Task<int?> ChangePassword(AccountInformationDTOChangePassword account)
        {
            string dynamicSalt;
            string hashedPassword;
            (dynamicSalt, hashedPassword) = MySecurityLib.Password.HashedPassword.GetHashedPassword(account.Password, staticSalt, "sha256");
            var editAccountId = await Database.AccountsInformation.ChangePassword(hashedPassword, account.AccountInformationId, dynamicSalt);
            Database.Commit();
            return editAccountId;
        }

            public async Task<string> PasswordRecovery(string email)
        {
            try
            {
                string randomPassword = GetRandomPassword();
                var account = await Database.AccountsInformation.FindEmail(email);
                (account.DynamicSalt, account.Password) = MySecurityLib.Password.HashedPassword.GetHashedPassword(randomPassword, staticSalt, "sha256");
                await Database.AccountsInformation.Update(account);
                Database.Commit();
                return randomPassword;
            }
            catch(Exception ex) {
                logger.Error("AccountInformationValidation.Validate - error:" + ex.Message);
                return null; }
           
        }


        public async Task<int?> Registration(AccountInformationDTOCreditionals account)
        {
            string dynamicSalt;
            (dynamicSalt, account.Password) = MySecurityLib.Password.HashedPassword.GetHashedPassword(account.Password, staticSalt, "sha256");
            var mappedAccount = new AutoMap<AccountInformation, AccountInformationDTOCreditionals>().Initialize(account);
            mappedAccount.DynamicSalt = dynamicSalt;
            var newUsersId = await Database.AccountsInformation.Create(mappedAccount);
            Database.Commit();
            return newUsersId;
        }
        
        public void Dispose()
        {
            Database.Dispose();
        }  
    }
}
