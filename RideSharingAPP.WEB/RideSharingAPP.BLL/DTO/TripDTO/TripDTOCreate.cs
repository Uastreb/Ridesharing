using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.DTO.TripDTO
{
    public class TripDTOCreate
    {
        public int NumberOfSeats { get; set; }
        public int Status { get; set; }
        public DateTime RegistrationEndDate { get; set; }

        public int CarId { get; set; }
        public int DriverId { get; set; }
    }
}
