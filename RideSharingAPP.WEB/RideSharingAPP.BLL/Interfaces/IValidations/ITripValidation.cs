using RideSharinAPP.COMMON.Erors;
using RideSharingAPP.BLL.DTO.TripDTO;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces.IValidations
{
    public interface ITripValidation
    {
        ErrorModel IsValid(TripDTOCreate trip);
        ErrorModel PointsIsValid(TripDTOPoints points);
        Task<ErrorModel> DeleteTripIsValid(int? tripId);
        Task<ErrorModel> CompleteTripIsValid(int? tripId);
        Task<ErrorModel> CheckId(int? tripId);
    }
}
