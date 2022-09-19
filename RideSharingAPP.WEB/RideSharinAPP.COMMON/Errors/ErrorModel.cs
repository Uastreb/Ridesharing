using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Erors
{
    public class ErrorModel
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ErrorModel(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}