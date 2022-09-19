using NLog;
using RideSharinAPP.COMMON.Erors;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.AccountInformationDTO;
using RideSharingAPP.BLL.Interfaces;
using RideSharingAPP.BLL.Interfaces.IValidations;
using System.Linq;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services.Validations
{
    public class AccountInformationValidation : IAccountInformationValidation
    {
        private readonly IUnitOfWork Database;
        private readonly AccountInformationEnumErrors validationHelper = new AccountInformationEnumErrors();
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public AccountInformationValidation(IUnitOfWork uow)
        {
            Database = uow;
        }


        public async Task<ErrorModel> IsIndividualEmail(string email)
        {
            if (email == null)
            {
                logger.Error("AccountInformationValidation.Validate - email is null");
                return validationHelper.InvalidData;
            }


            bool hasAccountWithSameName = (await Database.AccountsInformation.FindEmail(email)).Equals(null);
            if (hasAccountWithSameName)
            {
                logger.Error("AccountInformationValidation.Validate - another user has same email: {0}", email);
                return validationHelper.AnotherUserHasSameEmail;
            }
            return null;
        }

        public async Task<ErrorModel> IsValidPasswordRecovery(AccountInformationDTOGetEmail account)
        {
            if (account.Email == null)
            {
                logger.Error("AccountInformationValidation.Validate - email is null");
                return validationHelper.InvalidData;
            }


            bool accountExists = (await Database.AccountsInformation.FindEmail(account.Email)).Equals(null);
            if (accountExists)
            {
                logger.Error("AccountInformationValidation.Validate - the user with the specified mail does not exist: {0}", account.Email);
                return validationHelper.AnotherUserHasSameEmail;
            }
            return null;
        }


        public ErrorModel IsValidAuthorization(AccountInformationDTOCreditionals account)
        {
            if (string.IsNullOrEmpty(account.Email))
            {
                logger.Error("AccountInformationValidation.Validate - email is null");
                return validationHelper.InvalidData;
            }

            if (string.IsNullOrEmpty(account.Password))
            {
                logger.Error("AccountInformationValidation.Validate - password is null");
                return validationHelper.InvalidData;
            }

            if (account.Password.Count() < 6 || account.Password.Count() > 30)
            {
                logger.Error("AccountInformationValidation.Validate - incorrect password");
                return validationHelper.InvalidPassword;
            }
            return null;
        }

        public ErrorModel IsValidChangePassword(AccountInformationDTOChangePassword account)
        {
            if (string.IsNullOrEmpty(account.Password))
            {
                logger.Error("AccountInformationValidation.Validate - password is null");
                return validationHelper.InvalidData;
            }
            return null;
        }

        public async Task<ErrorModel> IsValidRegistration(AccountInformationDTOCreditionals account)
        {
            if (string.IsNullOrEmpty(account.Email))
            {
                logger.Error("AccountInformationValidation.Validate - email is null");
                return validationHelper.InvalidData;
            }

            if (string.IsNullOrEmpty(account.Password))
            {
                logger.Error("AccountInformationValidation.Validate - password is null");
                return validationHelper.InvalidData;
            }
            var searchAccount = await Database.AccountsInformation.FindEmail(account.Email);
            if (searchAccount != null)
            {
                logger.Error("AccountInformationValidation.Validate - another user has same email: {0}", account.Email);
                return validationHelper.AnotherUserHasSameEmail;
            }
            return null;
        }

    }
}
