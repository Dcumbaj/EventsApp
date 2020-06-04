using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApp.Models.DTOs
{
    public class ParticipantsDTO
    {
        public int ParticipantID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Adress { get; set; }
        public int CityID { get; set; }
    }
}