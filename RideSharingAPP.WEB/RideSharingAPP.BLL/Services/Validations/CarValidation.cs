using NLog;
using RideSharinAPP.COMMON.Enums;
using RideSharinAPP.COMMON.Erors;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.CarDTO;
using RideSharingAPP.BLL.Interfaces.IValidations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services.Validations
{
    public class CarValidation : ICarValidation
    {
        private readonly IUnitOfWork Database;
        private readonly CarEnumErrors validationHelper = new CarEnumErrors();
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public CarValidation(IUnitOfWork uow)
        {
            Database = uow;
        }

        public ErrorModel EditCarModelIsValid(CarDTOCreate car)
        {
            if (car.Comments.Length > 1000)
            {
                logger.Error("CarValidation.Validate - incorrect comments length");
                return validationHelper.InvalidData;
            }
            return null;
        }

        public async Task<ErrorModel> DeleteCarIsValid(int CarId)
        {
            var trips = await Database.Trips.GetAll();
            var sortedTrips = trips.Where(x => x.CarId == CarId).ToList();
            var activeTrips = sortedTrips.Where(x => x.Status == new Status().StatusIsActive.Code);
            if (activeTrips != null)
            {
                logger.Error("CarValidation.Validate - the deleted car has an active route");
                return validationHelper.HasActiveRoute;
            }
            return null;
        }

        public ErrorModel IsValid(CarDTOCreate car)
        {
            if (string.IsNullOrEmpty(car.Mark))
            {
                logger.Error("CarValidation.Validate - mark is null");
                return validationHelper.InvalidData;
            }
            if (string.IsNullOrEmpty(car.Model))
            {
                logger.Error("CarValidation.Validate - model is null");
                return validationHelper.InvalidData;
            }
            if (string.IsNullOrEmpty(car.RegistrationNumber))
            {
                logger.Error("CarValidation.Validate - registration number is null");
                return validationHelper.InvalidData;
            }
            if (car.YearOfIssue < 1960 || car.YearOfIssue > DateTime.Now.Year)
            {
                logger.Error("CarValidation.Validate - incorrect year");
                return validationHelper.InvalidData;
            }
            return null;
        }

    }
}
