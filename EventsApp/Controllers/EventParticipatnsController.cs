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
    public class EventParticipatnsController : ApiController
    {
        private EventEntites db = new EventEntites();
        
        // GET: api/EventParticipatns
        public IHttpActionResult GetEventParticipatns()
        {
            var eventParticipants = db.EventParticipatns
                    .Select(s => new EventParticipantsDTO()
                    {
                        EventParticipantsID = s.EventParticipantsID,
                        CityID = s.CityID,
                        EventID = s.EventID
                    }).ToList();

            if (eventParticipants.Count == 0)
            {
                return NotFound();
            }

            return Ok(eventParticipants);
        }

        // GET: api/EventParticipatns/5
        [ResponseType(typeof(EventParticipatns))]
        public IHttpActionResult GetEventParticipatns(int id)
        {
            var eventParticipants = db.EventParticipatns
                    .Where(s => s.EventParticipantsID == id)
                    .Select(s => new EventParticipantsDTO()
                    {
                        CityID = s.CityID,
                        EventID = s.EventID
                    }).SingleOrDefault();

            return Ok(eventParticipants);
        }

        // PUT: api/EventParticipatns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventParticipatns(int id, EventParticipatns eventParticipatns)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventParticipatns.EventParticipantsID)
            {
                return BadRequest("Nije dan valjan ID");
            }

            db.Entry(eventParticipatns).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventParticipatnsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EventParticipatns
        [ResponseType(typeof(EventParticipatns))]
        public IHttpActionResult PostEventParticipatns(EventParticipatns eventParticipatns)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventParticipatns.Add(new EventParticipatns()
            {
                CityID = eventParticipatns.EventID,
                EventID = eventParticipatns.EventID
            });

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/EventParticipatns/5
        [ResponseType(typeof(EventParticipatns))]
        public IHttpActionResult DeleteEventParticipatns(int id)
        {
            bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var eventParticipant = new EventParticipatns { EventParticipantsID = id };

                db.EventParticipatns.Attach(eventParticipant);
                db.Entry(eventParticipant).State = EntityState.Deleted;
                db.SaveChanges();
                return Ok("Izbrisan je event participant sa ovim ID-em: " + eventParticipant.EventParticipantsID);
            }
            catch (Exception e)
            {
                return BadRequest("Ne postoji to nešto sa ovim ID-em, kaj god ne da mi se više");
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

        private bool EventParticipatnsExists(int id)
        {
            return db.EventParticipatns.Count(e => e.EventParticipantsID == id) > 0;
        }
    }
}