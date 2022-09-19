using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Enums
{
    public class Status
    {
        private readonly StatusModel[] codeOfStatus =
        {
            new StatusModel(1, "Активен"),
            new StatusModel(2, "Завершен"),
            new StatusModel(3, "Удален")
        };

        public StatusModel StatusIsActive
        {
            get { return codeOfStatus.FirstOrDefault(x => x.Code == 1); }
        }

        public StatusModel StatusIsCompleted
        {
            get { return codeOfStatus.FirstOrDefault(x => x.Code == 2); }
        }

        public StatusModel StatusDeleted
        {
            get { return codeOfStatus.FirstOrDefault(x => x.Code == 3); }
        }
    }
}
