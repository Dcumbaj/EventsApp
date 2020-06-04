using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApp.Models.DTOs
{
    public class EventsDTO
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public bool Ongoing { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LocationID { get; set; }
    }
}