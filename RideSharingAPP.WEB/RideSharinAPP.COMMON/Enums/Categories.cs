using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharinAPP.COMMON.Enums
{
    public class Categories
    {
        private readonly CategoriesModel[] codeOfCategory =
        {
            new CategoriesModel(1, "A"),
            new CategoriesModel(2, "A, B"),
            new CategoriesModel(3, "A, B, C"),
            new CategoriesModel(4, "A, B, C, D"),
            new CategoriesModel(5, "A, B, C, D, M"),
            new CategoriesModel(6, "B"),
            new CategoriesModel(7, "B, C"),
            new CategoriesModel(8, "B, C, D"),
            new CategoriesModel(9, "B, C, D, M"),
            new CategoriesModel(10, "C"),
            new CategoriesModel(11, "C, D"),
            new CategoriesModel(12, "C, D, M"),
            new CategoriesModel(13, "D"),
            new CategoriesModel(14, "D, M"),
            new CategoriesModel(15, "M")
        };

        public CategoriesModel[] GetList()
        {
            return codeOfCategory;
        }
    }
}
