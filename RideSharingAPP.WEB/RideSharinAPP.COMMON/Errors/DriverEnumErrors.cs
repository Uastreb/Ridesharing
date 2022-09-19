using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Erors
{
    public class DriverEnumErrors
    {
        private readonly ErrorModel[] codeOfErrors =
        {
            new ErrorModel(201, "Ошибка! Введены некорректные данные."),
        };

        public ErrorModel InvalidData
        {
            get { return codeOfErrors.FirstOrDefault(x => x.Code == 201); }
        }
    }
}
