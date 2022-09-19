using NLog;
using RideSharinAPP.COMMON.Erors;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.DriverLicensesDTO;
using RideSharingAPP.BLL.Interfaces.IValidations;
using System;


namespace RideSharingAPP.BLL.Services.Validations
{
    public class DriverLicensesValidation : IDriverLicensesValidation
    {
        private readonly IUnitOfWork Database;
        private readonly DriverLicensesEnumErrors validationHelper = new DriverLicensesEnumErrors();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DriverLicensesValidation(IUnitOfWork uow)
        {
            Database = uow;
        }

        public ErrorModel IsValid(DriverLicensesDTOCreate driverLicenses)
        {
            if (string.IsNullOrEmpty(driverLicenses.IssuingAuthority))
            {
                logger.Error("DriverLicensesValidation.Validate - issuing authority is null");
                return validationHelper.InvalidData;
            }
            if (driverLicenses.DateOfReceiving.Year < 1900 || driverLicenses.DateOfReceiving > DateTime.Now )
            {
                logger.Error("DriverLicensesValidation.Validate - incorrect date of receiving");
                return validationHelper.InvalidData;
            }
            if (driverLicenses.ExpirationDate < DateTime.Now || driverLicenses.ExpirationDate.Year > DateTime.Now.Year + 40)
            {
                logger.Error("DriverLicensesValidation.Validate - incorrect date expiration date");
                return validationHelper.InvalidData;
            }
            if (driverLicenses.ExpirationDate < driverLicenses.DateOfReceiving)
            {
                logger.Error("DriverLicensesValidation.Validate - incorrect date expiration date or date of receiving");
                return validationHelper.InvalidData;
            }
            if (string.IsNullOrEmpty(driverLicenses.DriverLicensesNumber))
            {
                logger.Error("DriverLicensesValidation.Validate - number drivers license is null");
                return validationHelper.InvalidData;
            }
            return null;
        }
    }
}
