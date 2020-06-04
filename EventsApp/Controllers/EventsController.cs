using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EventsApp.Models;
using EventsApp.Models.DTOs;

namespace EventsApp.Controllers
{
    public class EventsController : ApiController
    {
        private EventEntites db = new EventEntites();

        // GET: api/Events
        public IHttpActionResult GetEvents()
        {
          var events = db.Events
                        .Select(s => new EventsDTO(){ 
                            EventID = s.EventID,
                            Name = s.Name,
                            Ongoing = s.Ongoing,
                            StartDate = s.StartDate,
                            EndDate = s.EndDate,
                            LocationID = s.LocationID
                        }).ToList();

            if (events.Count == 0)
            {
                return NotFound();
            }

            return Ok(events);
        }

        // GET: api/Events/5
        [ResponseType(typeof(Events))]
        public IHttpActionResult GetEvents(int id)
        {
            var events = db.Events.Where(s => s.EventID == id).Select(s => new EventsDTO()
            {
                EventID = s.EventID,
                Name = s.Name,
                Ongoing = s.Ongoing,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                LocationID = s.LocationID
            }).SingleOrDefault();

            return Ok(events);
        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvents(int id, Events events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != events.EventID)
            {
                return BadRequest();
            }

            db.Entry(events).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Events
        [ResponseType(typeof(Events))]
        public IHttpActionResult PostEvents(Events events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (events.StartDate > events.EndDate)
            {
                return BadRequest("Početak eventa ne može biti prije kraja eventa");
            }

            db.Events.Add(new Events()
            {
                EventID = events.EventID,
                Name = events.Name,
                Ongoing = events.Ongoing,
                StartDate = events.StartDate,
                EndDate = events.EndDate,
                LocationID = events.LocationID
            });

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Events/5
        [ResponseType(typeof(Events))]
        public IHttpActionResult DeleteEvents(int id)
        {
            bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var events = new Events { EventID = id };

                db.Events.Attach(events);
                db.Entry(events).State = EntityState.Deleted;
                db.SaveChanges();
                return Ok(events);
            }
            catch(Exception e)
            {
                return BadRequest("Event je vezan na neki entitet ili event ne postoji");
            }
            finally
            {
                db.Configuration.ValidateOnSaveEnabled = oldValidateOnSaveEnabled;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventsExists(int id)
        {
            return db.Events.Count(e => e.EventID == id) > 0;
        }
    }
}