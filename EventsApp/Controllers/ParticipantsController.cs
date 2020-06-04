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
    public class ParticipantsController : ApiController
    {
        private EventEntites db = new EventEntites();

        // GET: api/Participants
        public IHttpActionResult GetParticipants()
        {
            var praticipants = db.Participants
                        .Select(s => new ParticipantsDTO()
                        {
                            ParticipantID = s.ParticipantsID,
                            First_Name = s.First_Name,
                            Last_Name = s.Last_Name,
                            Adress = s.Adress,
                            CityID = s.CityID
                        }).ToList();

            if (praticipants.Count == 0)
            {
                return NotFound();
            }

            return Ok(praticipants);
        }

        // GET: api/Participants/5
        [ResponseType(typeof(Participants))]
        public IHttpActionResult GetParticipants(int id)
        {
            var participant = db.Participants
                     .Where(s => s.ParticipantsID == id)
                     .Select(s => new ParticipantsDTO()
                     {
                         ParticipantID = s.ParticipantsID,
                         First_Name = s.First_Name,
                         Last_Name = s.Last_Name,
                         Adress = s.Adress,
                         CityID = s.CityID
                     }).SingleOrDefault();

            return Ok(participant);
        }

        // PUT: api/Participants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipants(int id, Participants participants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participants.ParticipantsID)
            {
                return BadRequest();
            }

            db.Entry(participants).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantsExists(id))
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

        // POST: api/Participants
        [ResponseType(typeof(Participants))]
        public IHttpActionResult PostParticipants(Participants participants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Participants.Add(new Participants()
            {
                First_Name = participants.First_Name,
                Last_Name = participants.Last_Name,
                Adress = participants.Adress,
                CityID = participants.CityID
            });

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Participants/5
        [ResponseType(typeof(Participants))]
        public IHttpActionResult DeleteParticipants(int id)
        {
            bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var participant = new Participants { ParticipantsID = id };

                db.Participants.Attach(participant);
                db.Entry(participant).State = EntityState.Deleted;
                db.SaveChanges();
                return Ok(participant.First_Name + " " + participant.Last_Name);
            }
            catch (Exception e)
            {
                return BadRequest("Ne postoji participant sa ovim ID-em");
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

        private bool ParticipantsExists(int id)
        {
            return db.Participants.Count(e => e.ParticipantsID == id) > 0;
        }
    }
}