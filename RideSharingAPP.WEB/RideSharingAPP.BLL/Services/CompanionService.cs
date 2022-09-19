using RideSharinAPP.COMMON.Enums;
using RideSharingApp.BLL.Services;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.ClientDTO;
using RideSharingAPP.BLL.DTO.CompanionDTO;
using RideSharingAPP.BLL.DTO.TripDTO;
using RideSharingAPP.BLL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services
{
    public class CompanionService : ICompanionService
    {
        IUnitOfWork Database { get; set; }

        public CompanionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<IEnumerable<ClientDTOwithCompanionDate>> GetClients(int tripId)
        {
            var allClients = await Database.Clients.GetAll();
            var allCompanions = await Database.Companions.GetAll();
            var companionsForThatRoute = allCompanions.Where(x => x.TripID == tripId);

            List<ClientDTOwithCompanionDate> clientsForThatRoute = new List<ClientDTOwithCompanionDate>();
            foreach(var a in companionsForThatRoute)
            {
                var client = allClients.Where(x => x.id == a.ClientID).FirstOrDefault();
                var mappedClient = new AutoMap<ClientDTOwithCompanionDate, Client>().Initialize(client);
                mappedClient.EndCoordinates = a.EndCoordinates;
                mappedClient.TotalCost = a.TotalCost;
                clientsForThatRoute.Add(mappedClient);
            }
            return clientsForThatRoute;
        }

        public async Task<int?> RegistrationForTheRoute(CompanionDTOCreate companion)
        {
            var mappedCompanion = new AutoMap<Companion, CompanionDTOCreate>().Initialize(companion);
            var checkResult = await Database.Companions.Create(mappedCompanion);
            Database.Commit();
            return checkResult;
        }

        public async Task<string> GetDriverTelephone(int tripId)
        {
            var trip = await Database.Trips.Get(tripId);
            var driver = await Database.Drivers.Get(trip.DriverId);
            return driver.Telephone;
        }

        public async Task<bool?> Unregister(int companionId)
        {
            var checkResult = await Database.Companions.Delete(companionId);
            Database.Commit();
            return checkResult;
        }

        public async Task<bool?> DeletedForTheRoute(int id)
        {
            var checkResult = await Database.Companions.Delete(id);
            Database.Commit();
            return checkResult;
        }

        public async Task<IEnumerable<CompanionDTOCreate>> GetAll()
        {
            var companions = await Database.Companions.GetAll();
            var mappedCompanions = companions.Select(p => new AutoMap<CompanionDTOCreate, Companion>().Initialize(p)).ToList();
            return mappedCompanions;
        }

        public async Task<CompanionDTOCreate> Get(int companionId)
        {
            var companion = await Database.Companions.Get(companionId);
            var mappedCar = new AutoMap<CompanionDTOCreate, Companion>().Initialize(companion);
            return mappedCar;
        }

        public async Task<IEnumerable<CompanionDTOCreate>> GetActiveUserRoutes(int clientId)
        {
            var trips = await Database.Trips.GetAll();
            var companions = await Database.Companions.GetAll();
            var userCompanions = companions.Where(x => x.ClientID == clientId);
            List<CompanionDTOCreate> mappedCompanionList = new List<CompanionDTOCreate>();
            foreach (var x in userCompanions)
            {
                var temp = trips.Where(p => p.id == x.TripID && p.Status == new Status().StatusIsActive.Code).FirstOrDefault();

                if (temp == default)
                {
                    return default;
                }

                mappedCompanionList.Add(new CompanionDTOCreate
                {
                    TripID = temp.id,
                    ClientID = clientId,
                    OriginCoordinates = x.OriginCoordinates,
                    EndCoordinates = x.EndCoordinates,
                    DateAndTimeOfArrival = x.DateAndTimeOfArrival,
                    DateAndTimeOfDepartue = x.DateAndTimeOfDepartue,
                    TotalCost = x.TotalCost
                });
            }
            return mappedCompanionList.AsEnumerable();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
