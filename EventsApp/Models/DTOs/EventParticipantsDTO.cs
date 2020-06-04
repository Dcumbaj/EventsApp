using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApp.Models.DTOs
{
    public class EventParticipantsDTO
    {
        public int EventParticipantsID { get; set; }
        public int CityID { get; set; }
        public int EventID { get; set; }
    }
}