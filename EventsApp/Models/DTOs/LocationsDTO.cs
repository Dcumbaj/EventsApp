using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApp.Models.DTOs
{
    public class LocationsDTO
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int CityID { get; set; }
    }
}