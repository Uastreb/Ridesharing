using RideSharinAPP.COMMON.Erors;
using RideSharingAPP.BLL.DTO.AccountInformationDTO;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces.IValidations
{
    /// <summary>Account validation service interface.</summary>
    public interface IAccountInformationValidation
    {
        /// <summary>Validation of authorization model.</summary>
        ErrorModel IsValidAuthorization(AccountInformationDTOCreditionals account);

        /// <summary>Validation of registration model.</summary>
        Task<ErrorModel> IsValidRegistration(AccountInformationDTOCreditionals account);

        /// <summary>Validation of password recovery model.</summary>
        Task<ErrorModel> IsValidPasswordRecovery(AccountInformationDTOGetEmail account);

        /// <summary>Validation of password change model.</summary>
        ErrorModel IsValidChangePassword(AccountInformationDTOChangePassword account);
    }
}
