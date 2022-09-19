using System;
using System.Threading.Tasks;
using RideSharingAPP.BLL.DTO;
using RideSharingAPP.BLL.DTO.AccountInformationDTO;

namespace RideSharingApp.BLL.Interfaces
{
    public interface IAccountInformationService : IDisposable
    {
        Task<int?> Registration(AccountInformationDTOCreditionals account);
        Task<int?> Authorization(AccountInformationDTOCreditionals account);
        Task<string> PasswordRecovery(string email);
        Task<string> FindEmail(string email);
        Task<int?> ChangePassword(AccountInformationDTOChangePassword account);
    }
}
