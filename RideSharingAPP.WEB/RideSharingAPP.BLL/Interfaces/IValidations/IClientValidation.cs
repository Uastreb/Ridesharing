using RideSharinAPP.COMMON.Erors;
using RideSharingAPP.BLL.DTO.ClientDTO;

namespace RideSharingAPP.BLL.Interfaces.IValidations
{
    /// <summary>Client validation service interface.</summary>
    public interface IClientValidation
    {
        /// <summary>Customer model validation.</summary>
        ErrorModel IsValid(ClientDTOCreate client);
    }
}
