using RideSharingAPP.BLL.DTO.PassingPointDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces
{
    public interface IPassingPointService : IDisposable
    {
        Task<IEnumerable<int?>> Create(IEnumerable<PassingPointDTOCreate> passingPoint, int? TripsId);
        Task<IEnumerable<PassingPointDTOCreate>> GetAll();
        Task<IEnumerable<PassingPointDTOCreate>> GetRoutesPassingPoints(int routeId);
    }
}
