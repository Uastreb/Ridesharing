using System;

namespace RideSharingApp.DAL.Entities
{
    public class Client
    {
        public int id { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Comments { get; set; }
        public bool Enabled { get; set; }
        public string ReasonForBlocking { get; set; }


        public int AccountInformationID { get; set; }
    }
}
