using NLog;
using RideSharinAPP.COMMON.Erors;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.TripDTO;
using RideSharingAPP.BLL.Interfaces.IValidations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services.Validations
{
    public class TripValidation : ITripValidation
    {
        private readonly IUnitOfWork Database;
        private readonly TripEnumErrors validationHelper = new TripEnumErrors();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TripValidation(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<ErrorModel> CheckId(int? tripId)
        {
            if (tripId == null)
            {
                logger.Error("TripValidation.Validate - trips id is null");
                return validationHelper.TripIdIsNull;
            }

            var trip = await Database.Trips.Get(Convert.ToInt32(tripId));
            if (trip == null)
            {
                logger.Error("TripValidation.Validate - trip is not exist");
                return validationHelper.TripIsNotExist;
            }
            return null;
        }

        public async Task<ErrorModel> CompleteTripIsValid(int? tripId)
        {
            var errorId = await CheckId(tripId);
            if (errorId != null)
            {
                return errorId;
            }

            var points = await Database.PassingPoints.GetAll();
            var tripPoints = points.Where(x => x.TripID == tripId);
            if (tripPoints.LastOrDefault().DateAndTimeOfArrival < DateTime.Now )
            {
                logger.Error("TripValidation.Validate - today's date is earlier than the date of the last point");
                return validationHelper.InvalidDateOfCompleted;
            }
            return null; 
        }

        public async Task<ErrorModel> DeleteTripIsValid(int? tripId)
        {
            var errorId = await CheckId(tripId);
            if (errorId != null)
            {
                return errorId;
            }

            var companions = await Database.Companions.GetAll();
            var companionsOfThatTrip = companions.Where(x => x.TripID == tripId);
            if (companionsOfThatTrip.Count() > 0)
            {
                logger.Error("TripValidation.Validate - users have already registered for this route");
                return validationHelper.RegisteredUsers;
            }
            return null;
        }

        public ErrorModel PointsIsValid (TripDTOPoints points)
        {
            if (string.IsNullOrEmpty(points.EndCoordinates))
            {
                logger.Error("TripValidation.Validate - end coordinates is null");
                return validationHelper.InvalidPoints;
            }
            if(string.IsNullOrEmpty(points.OriginCoordinates))
            {
                logger.Error("TripValidation.Validate - origin coordinates is null");
                return validationHelper.InvalidPoints;
            }
            return null;
        }

        public ErrorModel IsValid(TripDTOCreate trip)
        {
            if (trip.NumberOfSeats == 0)
            {
                logger.Error("TripValidation.Validate - number of seats is 0");
                return validationHelper.InvalidData;
            }
            if (trip.RegistrationEndDate == null)
            {
                logger.Error("TripValidation.Validate - registration end date are null");
                return validationHelper.InvalidData;
            }
            return null;
        }
    }
}
