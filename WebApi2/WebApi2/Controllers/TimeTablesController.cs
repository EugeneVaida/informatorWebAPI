﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    public class TimeTablesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        DateTime thisDay = DateTime.Today;

        // GET: api/TimeTables
        [AllowAnonymous]
        public IQueryable<TimeTable> GetTimeTables()
        {
            return db.TimeTables;
        }

        // GET: api/TimeTables/5
        [ResponseType(typeof(TimeTable))]
        public IHttpActionResult GetTimeTable(int id)
        {
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable == null)
            {
                return NotFound();
            }

            return Ok(timeTable);
        }

        // GET: api/GetTodayTimeTable
        [AllowAnonymous]
        [ResponseType(typeof(TimeTable))]
        [Route("api/GetTodayTimeTable")]
        public IQueryable<TimeTable> GetTodayTimeTable()
        {
            var today = thisDay.ToString("d");
            return db.TimeTables.Where( x => x.Date.Contains(today));
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/TimeTables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTimeTable(int id, TimeTable timeTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timeTable.Id)
            {
                return BadRequest();
            }

            db.Entry(timeTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeTableExists(id))
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

        [Authorize(Roles = "Admin")]
        // POST: api/TimeTables
        [ResponseType(typeof(TimeTable))]
        public IHttpActionResult PostTimeTable(TimeTable timeTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TimeTables.Add(timeTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = timeTable.Id }, timeTable);
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/TimeTables/5
        [ResponseType(typeof(TimeTable))]
        public IHttpActionResult DeleteTimeTable(int id)
        {
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable == null)
            {
                return NotFound();
            }

            db.TimeTables.Remove(timeTable);
            db.SaveChanges();

            return Ok(timeTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimeTableExists(int id)
        {
            return db.TimeTables.Count(e => e.Id == id) > 0;
        }
    }
}