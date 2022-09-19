using RideSharinAPP.COMMON.Erors;
using RideSharingAPP.BLL.DTO.CompanionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Interfaces.IValidations
{
    /// <summary>Companion validation service interface.</summary>
    public interface ICompanionValidation
    {

        /// <summary>check that the user cannot register on his own route, register on a route 
        /// where there are no seats, register on the route for which the registration date has ended.</summary>
        Task<ErrorModel> IsValid(int clientId, int tripId, int accountId);

        /// <summary>Сheck if there is a companion with such id and if id is not null.</summary>
        Task<ErrorModel> CheckId(int? companionId);
    }
}
