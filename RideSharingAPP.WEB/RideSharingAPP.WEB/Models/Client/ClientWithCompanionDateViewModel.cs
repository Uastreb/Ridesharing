using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RideSharingAPP.WEB.Models.Client
{
    public class ClientWithCompanionDateViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Comments { get; set; }

        public int AccountInformationID { get; set; }

        public decimal TotalCost { get; set; }
        public string EndCoordinates { get; set; }
    }
}