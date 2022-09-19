using RideSharingApp.BLL.Services;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.PassingPointDTO;
using RideSharingAPP.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services
{
    public class PassingPointService : IPassingPointService
    {
        IUnitOfWork Database { get; set; }

        public PassingPointService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<IEnumerable<int?>> Create(IEnumerable<PassingPointDTOCreate> passingPoint, int? TripsId)
        {
            List<int?> newPassingPointsId = new List<int?>();
            foreach (var a in passingPoint)
            {
                var mappedPassingPoint = new AutoMap<PassingPoint, PassingPointDTOCreate>().Initialize(a);
                var newPassingPointId = await Database.PassingPoints.Create(mappedPassingPoint);
                newPassingPointsId.Add(newPassingPointId);
            }
            Database.Commit();
            return newPassingPointsId.AsEnumerable();
        }

        public async Task<IEnumerable<PassingPointDTOCreate>> GetAll()
        {
            var passingPoints = await Database.PassingPoints.GetAll();
            var mappedPassingPoints = new AutoMap<PassingPointDTOCreate, PassingPoint>().GetAll(passingPoints);
            return mappedPassingPoints;
        }

        public async Task<IEnumerable<PassingPointDTOCreate>> GetRoutesPassingPoints(int routeId)
        {
            var passingPoints = await Database.PassingPoints.GetAll();  
            var mappedPassingPoints = new AutoMap<PassingPointDTOCreate, PassingPoint>().GetAll(passingPoints);
            var sortedPassingPoints = mappedPassingPoints.Where(x => x.TripID == routeId);
            return sortedPassingPoints;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
