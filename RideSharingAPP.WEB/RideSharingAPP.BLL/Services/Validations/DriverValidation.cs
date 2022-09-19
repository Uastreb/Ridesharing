using NLog;
using RideSharinAPP.COMMON.Erors;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.DriverDTO;
using RideSharingAPP.BLL.Interfaces.IValidations;
using System;

namespace RideSharingAPP.BLL.Services.Validations
{
    public class DriverValidation : IDriverValidation
    {
        private readonly IUnitOfWork Database;
        private readonly DriverEnumErrors validationHelper = new DriverEnumErrors();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DriverValidation(IUnitOfWork uow)
        {
            Database = uow;
        }

        public ErrorModel IsValid(DriverDTOCreate driver)
        {
            if (string.IsNullOrEmpty(driver.Name))
            {
                logger.Error("DriverValidation.Validate - name is null");
                return validationHelper.InvalidData;
            }
            if (string.IsNullOrEmpty(driver.Surname))
            {
                logger.Error("DriverValidation.Validate - surname is null");
                return validationHelper.InvalidData;
            }
            if (string.IsNullOrEmpty(driver.Patronymic))
            {
                logger.Error("DriverValidation.Validate - patronymic number is null");
                return validationHelper.InvalidData;
            }
            if (string.IsNullOrEmpty(driver.Telephone))
            {
                logger.Error("DriverValidation.Validate - telephone number is null");
                return validationHelper.InvalidData;
            }
            if (driver.DateOfBirth.Year < 1900 || driver.DateOfBirth.Year > DateTime.Now.Year)
            {
                logger.Error("DriverValidation.Validate - incorrect date of birth");
                return validationHelper.InvalidData;
            }
            return null;
        }
    }
}
