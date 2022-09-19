using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.DTO.PassingPointDTO
{
    public class PassingPointDTOCreate
    {
        public int id { get; set; }

        public string OriginCoordinates { get; set; }
        public string EndCoordinates { get; set; }
        public DateTime DateAndTimeOfDepartue { get; set; }
        public DateTime DateAndTimeOfArrival { get; set; }
        public decimal Cost { get; set; }
        public int PointNumberOfThisTrip { get; set; }

        public int TripID { get; set; }
    }
}
