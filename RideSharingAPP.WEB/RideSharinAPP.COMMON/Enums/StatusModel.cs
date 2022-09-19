using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Enums
{
    public class StatusModel
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public StatusModel(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
