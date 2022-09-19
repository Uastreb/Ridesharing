using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Erors
{
    public class CarEnumErrors
    {
        private readonly ErrorModel[] codeOfErrors =
        {
            new ErrorModel(201, "Ошибка! Введены некорректные данные."),
            new ErrorModel(202, "Удаляемый автомобиль имеет активный маршрут")
        };

        public ErrorModel InvalidData
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 201); }
        }

        public ErrorModel HasActiveRoute
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 202); }
        }
    }
}
