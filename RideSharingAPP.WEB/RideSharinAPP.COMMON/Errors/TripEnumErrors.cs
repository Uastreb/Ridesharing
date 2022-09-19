using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Erors
{
    public class TripEnumErrors
    {
        private readonly ErrorModel[] codeOfErrors =
       {
            new ErrorModel(201, "Ошибка! Введены некорректные данные."),
            new ErrorModel(202, "Поля с точками отправлния, прибытия не заполнены"),
            new ErrorModel(203, "Нельзя удалить маршрут, так как сегодняшняя дата раньше даты назнченой на завершение последней точки маршрута"),
            new ErrorModel(204, "Нельзя удалить данный маршрут, так как на его уже зарегестрированы пользователи"),
            new ErrorModel(205, "Переданный элемент пуст"),
            new ErrorModel(206, "Указанного маршрута не существует")
        };

        public ErrorModel InvalidData
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 201); }
        }

        public ErrorModel InvalidPoints
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 202); }
        }

        public ErrorModel InvalidDateOfCompleted
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 203); }
        }

        public ErrorModel RegisteredUsers
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 204); }
        }

        public ErrorModel TripIdIsNull
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 205); }
        }

        public ErrorModel TripIsNotExist
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 206); }
        }
    }
}
