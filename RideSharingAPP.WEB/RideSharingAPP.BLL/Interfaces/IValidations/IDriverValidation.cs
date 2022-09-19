using RideSharinAPP.COMMON.Erors;
using RideSharingAPP.BLL.DTO.DriverDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces.IValidations
{
    public interface IDriverValidation
    {
        ErrorModel IsValid(DriverDTOCreate driver);
    }
}
