using NLog;
using RideSharinAPP.COMMON.Erors;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.PassingPointDTO;
using RideSharingAPP.BLL.Interfaces.IValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingAPP.BLL.Services.Validations
{
    public class PassingPointValidation : IPassingPointValidation
    {
        private readonly IUnitOfWork Database;
        private readonly PassingPointEnumErrors validationHelper = new PassingPointEnumErrors();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public PassingPointValidation(IUnitOfWork uow)
        {
            Database = uow;
        }

        public ErrorModel IsValid(ICollection<PassingPointDTOCreate> passingPoints)
        {
            var passingPointsList = passingPoints.ToList();
            for (int i = 0; i < passingPoints.Count; i++)
            {

                if (string.IsNullOrEmpty(passingPointsList[i].OriginCoordinates))
                {
                    logger.Error("PassingPointValidation.Validate - origin coordinates is null");
                    return validationHelper.InvalidData;
                }
                if (string.IsNullOrEmpty(passingPointsList[i].EndCoordinates))
                {
                    logger.Error("PassingPointValidation.Validate - end coordinates is null");
                    return validationHelper.InvalidData;
                }
                if (passingPointsList[i].DateAndTimeOfArrival < DateTime.Now || passingPointsList[i].DateAndTimeOfDepartue > passingPointsList[i].DateAndTimeOfArrival ||
                    passingPointsList[i].DateAndTimeOfArrival.Month > DateTime.Now.Month + 3 || passingPointsList[i].DateAndTimeOfDepartue.Month > DateTime.Now.Month + 3)
                {
                    logger.Error("PassingPointValidation.Validate - incorrect date or time of arrival");
                    return validationHelper.InvalidData;
                }
                if (passingPointsList[i].DateAndTimeOfDepartue < DateTime.Now)
                {
                    logger.Error("PassingPointValidation.Validate - date or time of departue are null");
                    return validationHelper.InvalidData;
                }
                if (passingPointsList[i].Cost <= 0)
                {
                    logger.Error("PassingPointValidation.Validate - incorrect cost");
                    return validationHelper.InvalidData;
                }
            }
            return null;
        }
    }
}
