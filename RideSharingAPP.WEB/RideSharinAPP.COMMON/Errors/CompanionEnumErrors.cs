using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Erors
{
    public class CompanionEnumErrors
    {
        private readonly ErrorModel[] codeOfErrors =
        {
            new ErrorModel(201, "Введены некорректные данные."),
            new ErrorModel(202, "Вы уже зарегестрированы на активный маршрут"),
            new ErrorModel(203, "Указанного обьекта не существует"),
            new ErrorModel(204, "Дата окончания регистрации на маршрут уже прошла"),
            new ErrorModel(205, "На маршрут уже зарегистрировано максимально возможное колличество людей"),
            new ErrorModel(206, "Вы не можете зарегистрироваться на свой же маршрут")
        };


        public ErrorModel InvalidData
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 201); }
        }

        public ErrorModel AlreadyRegistered
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 202); }
        }

        public ErrorModel CompanionIsNotExist
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 203); }
        }

        public ErrorModel RegistrationDateHasPassed
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 204); }
        }

        public ErrorModel AllSeatsOccupied
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 205); }
        }

        public ErrorModel OwnRoute
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 206); }
        }
    }
}
