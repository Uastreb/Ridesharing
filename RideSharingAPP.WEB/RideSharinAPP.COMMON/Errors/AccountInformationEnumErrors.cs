using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Erors
{
    public class AccountInformationEnumErrors
    {
        private readonly ErrorModel[] codeOfErrors =
        {
            new ErrorModel(201, "Ошибка! Введены некорректные данные."),
            new ErrorModel(202, "Ошибка! При регистрации произошла ошибка."),
            new ErrorModel(203, "Ошибка! Аккаунт с указанной почтой уже существует."),
            new ErrorModel(204, "Ошибка! Аккаунта с подобными данными не существует."),
            new ErrorModel(205, "Ошибка! При отправке сообщения на почту произошла ошибка."),
            new ErrorModel(206, "Длина пароля должна быть от 6 до 30 символов.")
        };

        public ErrorModel InvalidData
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 201); }
        }

        public ErrorModel RegistrationEror
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 202); }
        }

        public ErrorModel AnotherUserHasSameEmail
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 203); }
        }

        public ErrorModel UserNotFound
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 204); }
        }

        public ErrorModel SendEmailEror
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 205); }
        }

        public ErrorModel InvalidPassword
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 206); }
        }
    }
}
