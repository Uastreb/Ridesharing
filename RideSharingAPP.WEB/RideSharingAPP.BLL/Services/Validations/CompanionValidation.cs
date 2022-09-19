using NLog;
using RideSharinAPP.COMMON.Enums;
using RideSharinAPP.COMMON.Erors;
using RideSharingApp.BLL.Services;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.CompanionDTO;
using RideSharingAPP.BLL.DTO.TripDTO;
using RideSharingAPP.BLL.Interfaces;
using RideSharingAPP.BLL.Interfaces.IValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services.Validations
{
    public class CompanionValidation : ICompanionValidation
    {
        private readonly IUnitOfWork Database;
        private readonly CompanionEnumErrors validationHelper = new CompanionEnumErrors();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ICompanionService companionService;


        public CompanionValidation(IUnitOfWork uow, ICompanionService companionServ)
        {
            companionService = companionServ;
            Database = uow;
        }

        public async Task<ErrorModel> CheckId(int? companionId)
        {
            if (companionId == null)
            {
                logger.Error("CompanionValidation.Validate - companions id is null");
                return validationHelper.CompanionIsNotExist;
            }

            var companion = await Database.Companions.Get(Convert.ToInt32(companionId));
            if (companion == null)
            {
                logger.Error("CompanionValidation.Validate - companion is not exist");
                return validationHelper.CompanionIsNotExist;
            }
            return null;
        }

        public async Task<ErrorModel> IsValid(int clientId, int tripId, int accountId)
        {
            var usersActiveTrips = await companionService.GetActiveUserRoutes(clientId);
            if (usersActiveTrips.Count() != 0)
            {
                logger.Error("CompanionValidation.Validate - user has active trips");
                return validationHelper.AlreadyRegistered;
            }

            var trip = await Database.Trips.Get(tripId);
            var companions = await Database.Companions.GetAll();
            var thatRouteCompanions = companions.Where(p => p.TripID == tripId);
            var numberOfSeats = trip.NumberOfSeats - thatRouteCompanions.Count();
            if (numberOfSeats < 1)
            {
                logger.Error("CompanionValidation.Validate - not free seats");
                return validationHelper.AllSeatsOccupied;
            }

            //var drivers = await Database.Drivers.GetAll();
            //var thatAccount = drivers.Where(x => x.AccountInformationID == accountId);
            //var checkAccount = drivers.Where(x => x.id == trip.DriverId);
            //if (checkAccount != null)
            //{
            //    logger.Error("CompanionValidation.Validate - attempt to register on your own route");
            //    return validationHelper.OwnRoute;
            //}

            //if (trip.RegistrationEndDate < DateTime.Now)
            //{
            //    logger.Error("CompanionValidation.Validate - registration date has passed");
            //    return validationHelper.RegistrationDateHasPassed;
            //}

            return null;
        }
    }
}
