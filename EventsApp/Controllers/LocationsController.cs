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
    public class LocationsController : ApiController
    {
        private EventEntites db = new EventEntites();

        // GET: api/Locations
        public IHttpActionResult GetLocations()
        {
            var locations = db.Locations
                        .Select(s => new LocationsDTO()
                        {
                            LocationID = s.LocationID,
                            Name = s.Name,
                            Adress = s.Adress,
                            CityID = s.CityID
                        }).ToList();

            if (locations.Count == 0)
            {
                return NotFound();
            }

            return Ok(locations);
        }

        // GET: api/Locations/5
        [ResponseType(typeof(Locations))]
        public IHttpActionResult GetLocations(int id)
        {
            var location = db.Locations
                   .Where(s => s.LocationID == id)
                   .Select(s => new LocationsDTO()
                   {
                       LocationID = s.LocationID,
                       Name = s.Name,
                       Adress = s.Adress,
                       CityID = s.CityID
                   }).SingleOrDefault();

            return Ok(location);
        }

        // PUT: api/Locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocations(int id, Locations locations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locations.LocationID)
            {
                return BadRequest("Nije dan valjan ID");
            }

            db.Entry(locations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationsExists(id))
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

        // POST: api/Locations
        [ResponseType(typeof(Locations))]
        public IHttpActionResult PostLocations(Locations location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(new Locations()
            {
                Name = location.Name,
                Adress = location.Adress,
                CityID = location.CityID
            });

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Locations/5
        [ResponseType(typeof(Locations))]
        public IHttpActionResult DeleteLocations(int id)
        {
            bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var location = new Locations { LocationID = id };

                db.Locations.Attach(location);
                db.Entry(location).State = EntityState.Deleted;
                db.SaveChanges();
                return Ok(location.Name);
            }
            catch (Exception e)
            {
                return BadRequest("Ne postoji lokacija sa ovim ID-em");
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

        private bool LocationsExists(int id)
        {
            return db.Locations.Count(e => e.LocationID == id) > 0;
        }
    }
}