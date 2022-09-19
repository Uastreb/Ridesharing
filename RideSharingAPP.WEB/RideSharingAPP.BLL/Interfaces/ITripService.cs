using RideSharingAPP.BLL.DTO.TripDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces
{
    public interface ITripService : IDisposable
    {
        Task<IEnumerable<TripDTOList>> GetAll();
        Task<int?> Create(TripDTOCreate trip);
        Task<IEnumerable<TripDTOList>> GetDriversTrips(int driverId);
        Task<IEnumerable<TripDTOSearched>> SearchTrips(TripDTOPoints points);
        Task<int?> SetStatusCompleted(int tripId);
        Task<int?> SetStatusDeleted(int tripId);
    }
}
