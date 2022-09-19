using RideSharinAPP.COMMON.Erors;
using RideSharingAPP.BLL.DTO.PassingPointDTO;
using System.Collections.Generic;

namespace RideSharingAPP.BLL.Interfaces.IValidations
{
    public interface IPassingPointValidation
    {
        ErrorModel IsValid(ICollection<PassingPointDTOCreate> passingPoints);
    }
}
