using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.DTO.DriverLicensesDTO
{
    public class DriverLicensesDTOCreate
    {
        public int id { get; set; }
        public DateTime DateOfReceiving { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DriverLicensesNumber { get; set; }
        public string IssuingAuthority { get; set; }
        public int Categories { get; set; }

        public int DriverID { get; set; }
    }
}
