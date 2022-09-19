using NLog;
using RideSharinAPP.COMMON.Erors;
using RideSharingApp.DAL.Interfaces;
using RideSharingAPP.BLL.DTO.ClientDTO;
using RideSharingAPP.BLL.Interfaces.IValidations;
using System;


namespace RideSharingAPP.BLL.Services.Validations
{
    public class ClientValidation : IClientValidation
    {
        private readonly IUnitOfWork Database;
        private readonly ClientEnumErrors validationHelper = new ClientEnumErrors();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ClientValidation(IUnitOfWork uow)
        { 
            Database = uow;
        }

        public ErrorModel IsValid(ClientDTOCreate client)
        {
            if (string.IsNullOrEmpty(client.Name))
            {
                logger.Error("ClientValidation.Validate - name is null");
                return validationHelper.InvalidData;
            }
            if (string.IsNullOrEmpty(client.Surname))
            {
                logger.Error("ClientValidation.Validate - surname is null");
                return validationHelper.InvalidData;
            }
            if (string.IsNullOrEmpty(client.Patronymic))
            {
                logger.Error("ClientValidation.Validate - patronymic number is null");
                return validationHelper.InvalidData;
            }
            if (string.IsNullOrEmpty(client.Telephone))
            {
                logger.Error("ClientValidation.Validate - telephone number is null");
                return validationHelper.InvalidData;
            }
            if (client.DateOfBirth.Year < 1900 || client.DateOfBirth.Year > DateTime.Now.Year - 10)
            {
                logger.Error("ClientValidation.Validate - incorrect date of birth");
                return validationHelper.InvalidDateOfBirth;
            }
            return null;
        }
    }
}
