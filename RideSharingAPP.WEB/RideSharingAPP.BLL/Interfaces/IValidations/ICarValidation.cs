using RideSharinAPP.COMMON.Erors;
using RideSharingAPP.BLL.DTO.CarDTO;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces.IValidations
{
    /// <summary>Car validation service interface.</summary>
    public interface ICarValidation
    {
        /// <summary>Validation of car model.</summary>
        ErrorModel IsValid(CarDTOCreate car);

        /// <summary>Validation of changes to the car comment.</summary>
        ErrorModel EditCarModelIsValid(CarDTOCreate car);

        /// <summary>Check for active vehicle routes before deletingt.</summary>
        Task<ErrorModel> DeleteCarIsValid(int CarId);
    }
}
