using AutoMapper;
using EventsApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApp.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Events, EventsDTO>();
            CreateMap<Cities, CitiesDTO>();
            CreateMap<EventParticipatns, EventParticipantsDTO>();
            CreateMap<Locations, LocationsDTO>();
            CreateMap<Participants, ParticipantsDTO>();
        }
    }
}