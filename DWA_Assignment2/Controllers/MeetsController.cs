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
using DWA_Assignment2.Models;

namespace DWA_Assignment2.Controllers
{
    public class MeetsController : ApiController
    {
        private IRepository<Meet, SearchMeetViewModel> meetRP;

        public MeetsController()
        {
            meetRP = new MeetRepository();
        }

        public MeetsController(IRepository<Meet, SearchMeetViewModel> repository)
        {
            meetRP = repository;
        }

        // GET: api/Meets
        public List<Meet> GetMeets()
        {
            return meetRP.ToList();
        }

        // GET: api/Meets/5
        [ResponseType(typeof(Meet))]
        public IHttpActionResult GetMeet(int id)
        {
            Meet meet = meetRP.Find(id);
            if (meet == null)
            {
                return NotFound();
            }

            return Ok(meet);
        }

        //// PUT: api/Meets/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutMeet(int id, Meet meet)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != meet.MeetId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(meet).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MeetExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Meets
        [Authorize(Roles = "Club Official")]
        [ResponseType(typeof(Meet))]
        public IHttpActionResult PostMeet(MeetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Event> eventList = new List<Event>();

            var meet = new Meet
            {
                MeetName = model.Name,
                Venue = model.Venue,
                Date = model.Date,
                PoolLength = model.PoolLength,
                Events = eventList
            };

            meetRP.Add(meet);

            return CreatedAtRoute("DefaultApi", new { id = meet.MeetId }, meet);
        }

        //// DELETE: api/Meets/5
        //[ResponseType(typeof(Meet))]
        //public IHttpActionResult DeleteMeet(int id)
        //{
        //    Meet meet = db.Meets.Find(id);
        //    if (meet == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Meets.Remove(meet);
        //    db.SaveChanges();

        //    return Ok(meet);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool MeetExists(int id)
        {
            return meetRP.Count(id);
        }
    }
}