using RideSharinAPP.COMMON.Erors;
using RideSharingAPP.BLL.DTO.DriverLicensesDTO;

namespace RideSharingAPP.BLL.Interfaces.IValidations
{
    public interface IDriverLicensesValidation
    {
        /// <summary>Checking the correctness of the date of issue of rights and filling in all other fields.</summary>
        ErrorModel IsValid(DriverLicensesDTOCreate driverLicenses);
    }
}
