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
    public class CitiesController : ApiController
    {
        private EventEntites db = new EventEntites();

        // GET: api/Cities
        public IHttpActionResult GetCities()
        {
            var cities = db.Cities
                        .Select(s => new CitiesDTO()
                        {
                            CityID = s.CityID,
                            Name = s.Name
                        }).ToList();
            
            if (cities.Count == 0)
            {
                return NotFound();
            }

            return Ok(cities);
        }

        // GET: api/Cities/5
        [ResponseType(typeof(Cities))]
        public IHttpActionResult GetCities(int id)
        {
            var city = db.Cities
                    .Where(s => s.CityID == id)
                    .Select(s => new CitiesDTO()
                    {
                        CityID = s.CityID,
                        Name = s.Name
                    }).SingleOrDefault();

            if (city == null)
            {
                return BadRequest("Ne postoji grad sa ovim ID-em");
            }
            
            return Ok(city);
        }

        // PUT: api/Cities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCities(int id, Cities cities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cities.CityID)
            {
                return BadRequest("Nije dan valjan ID");
            }

            db.Entry(cities).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitiesExists(id))
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

        // POST: api/Cities
        [ResponseType(typeof(Cities))]
        public IHttpActionResult PostCities(CitiesDTO city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cities.Add(new Cities()
            {
                Name = city.Name
            });

            var cityName = db.Cities.Where(s => s.Name == city.Name).FirstOrDefault();
            if (cityName != null)
            {
                return BadRequest("Grad sa ovim imenom već postoji");
            }

            db.SaveChanges();
            
            return Ok(city.Name + " uspješno spremljen");
        }

        // DELETE: api/Cities/5
        [ResponseType(typeof(Cities))]
        public IHttpActionResult DeleteCities(int id)
        {
            bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var city = new Cities { CityID = id };

                db.Cities.Attach(city);
                db.Entry(city).State = EntityState.Deleted;
                db.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest("Grad je vezan na neki entitet ili grad ne postoji");
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

        private bool CitiesExists(int id)
        {
            return db.Cities.Count(e => e.CityID == id) > 0;
        }
    }
}