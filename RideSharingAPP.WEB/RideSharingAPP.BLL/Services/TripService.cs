using RideSharinAPP.COMMON.Enums;
using RideSharingApp.BLL.Services;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.CarDTO;
using RideSharingAPP.BLL.DTO.PassingPointDTO;
using RideSharingAPP.BLL.DTO.TripDTO;
using RideSharingAPP.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services
{
    public class TripService : ITripService
    {
        readonly Status statisHelper = new Status();
        IUnitOfWork Database { get; set; }

        public TripService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<int?> SetStatusDeleted(int tripId)
        {
            var trip = await Database.Trips.Get(tripId);
            trip.Status = new Status().StatusDeleted.Code;
            var editTripsId = await Database.Trips.Update(trip);
            return editTripsId;
        }

        public async Task<int?> SetStatusCompleted(int tripId)
        {
            var trip = await Database.Trips.Get(tripId);
            trip.Status = new Status().StatusIsCompleted.Code;
            var editTripsId = await Database.Trips.Update(trip);
            return editTripsId;
        }

        public async Task<int?> Create(TripDTOCreate trip)
        {
            var mappedTrip = new AutoMap<Trip, TripDTOCreate>().Initialize(trip);
            mappedTrip.Status = statisHelper.StatusIsActive.Code;
            int? newTripId = await Database.Trips.Create(mappedTrip);
            Database.Commit();
            return newTripId;
        }

        public async Task<IEnumerable<TripDTOSearched>> SearchTrips(TripDTOPoints points)
        {
            var allTrips = await Database.Trips.GetAll();
            var allTripsId = allTrips.Select(x => x.id).ToList();
            var allPoints = await Database.PassingPoints.GetAll();
            var firsTripsPoints = allTripsId.Select(x => allPoints.FirstOrDefault(p => p.TripID == x)).ToList();
            var relevanteTripsWithOriginCoordinates = firsTripsPoints.Where(x => x.OriginCoordinates == points.OriginCoordinates).ToList();

            var relevanteTripsWithEndPoint = relevanteTripsWithOriginCoordinates.Where(x => allPoints.Where(p => p.TripID == x.TripID && p.EndCoordinates == points.EndCoordinates).ToList().Count != 0).ToList();
            var relevanteTripsId = relevanteTripsWithEndPoint.Select(x => x.TripID);

            List<TripDTOSearched> returnedList = new List<TripDTOSearched>();
            
            foreach (var x in relevanteTripsId)
            {
                var tripPoints = allPoints.Where(p => p.TripID == x).ToList();
                decimal cost = 0;
                foreach (var a in tripPoints)
                {
                    cost += a.Cost;
                    if(a.EndCoordinates == points.EndCoordinates)
                    {
                        var relevanteTrip = allTrips.FirstOrDefault(n => n.id == a.TripID);
                        var mappedTrip = new AutoMap<TripDTOSearched, Trip>().Initialize(relevanteTrip);
                        mappedTrip.TotalСost = cost;
                        var mappedPoints = tripPoints.Select(o => new AutoMap<TripDTOPoints, PassingPoint>().Initialize(o));
                        mappedTrip.PassingPoints = mappedPoints;
                        mappedTrip.DateAndTimeOfDepartue = tripPoints.Where(n => n.OriginCoordinates == points.OriginCoordinates).First().DateAndTimeOfDepartue;
                        mappedTrip.DateAndTimeOfArrival = tripPoints.Where(n => n.EndCoordinates == points.EndCoordinates).First().DateAndTimeOfArrival;
                        mappedTrip.OriginCoordinates = tripPoints.Where(n => n.OriginCoordinates == points.OriginCoordinates).First().OriginCoordinates;
                        mappedTrip.EndCoordinates = tripPoints.Where(n => n.EndCoordinates == points.EndCoordinates).First().EndCoordinates;
                        returnedList.Add(mappedTrip);
                        break;
                    }
                }
            }
            return returnedList.AsEnumerable();
        }

        public async Task<IEnumerable<TripDTOList>> GetDriversTrips(int driverId)
        {
            var trips = await Database.Trips.GetAll();
            var mappedTrips = trips.Select(p => new AutoMap<TripDTOList, Trip>().Initialize(p)).ToList();
            var sortTrips = mappedTrips.Where(x => x.DriverId == driverId).ToList();
            return sortTrips.AsEnumerable();
        }

    public async Task<IEnumerable<TripDTOList>> GetAll()
        {
            var trips = await Database.Trips.GetAll();
            return trips.Select(p => new AutoMap<TripDTOList, Trip>().Initialize(p)).ToList();
        }

        public void Dispose()
        {
            Database.Dispose();
        }


    }
}
