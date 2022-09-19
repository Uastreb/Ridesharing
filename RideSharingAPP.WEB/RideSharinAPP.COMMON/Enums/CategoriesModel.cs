using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Enums
{
    public class CategoriesModel
    {
        public int idCategory { get; set; }
        public string Categories { get; set; }

        public CategoriesModel(int code, string category)
        {
            idCategory = code;
            Categories = category;
        }
    }
}
