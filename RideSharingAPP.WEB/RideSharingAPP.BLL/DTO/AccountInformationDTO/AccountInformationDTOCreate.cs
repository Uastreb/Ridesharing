using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.DTO.AccountInformationDTO
{
    public class AccountInformationDTOCreate
    {
        public int id;
        public string Email { get; set; }
        public string Password { get; set; }
        public string DynamicSalt { get; set; }
    }
}
