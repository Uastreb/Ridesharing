using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.PassingPoint
{
    public class PassingPointForSearhedListViewModel
    {
        public int id { get; set; }

        public string OriginCoordinates { get; set; }
        public string EndCoordinates { get; set; }
    }
}