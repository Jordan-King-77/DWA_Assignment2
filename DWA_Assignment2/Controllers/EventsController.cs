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
    public class EventsController : ApiController
    {
        private IRepository<Event, SearchEventViewModel> eveRP;

        public EventsController()
        {
            eveRP = new EventRepository();
        }

        public EventsController(IRepository<Event, SearchEventViewModel> repository)
        {
            eveRP = repository;
        }

        // GET: api/Events
        public List<Event> GetEvents()
        {
            return eveRP.ToList();
        }

        // GET: api/Events/5
        [ResponseType(typeof(Event))]
        public IHttpActionResult GetEvent(int id)
        {
            Event @event = eveRP.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        //// PUT: api/Events/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutEvent(int id, Event @event)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != @event.EventId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(@event).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EventExists(id))
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

        // POST: api/Events
        [Authorize(Roles = "Club Official")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult PostEvent(EventViewModel model/*Event @event*/)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Lane> laneList = new List<Lane>();

            var eve = new Event
            {
                MeetId = model.MeetId,
                AgeRange = model.AgeRange,
                Distance = model.Distance,
                Stroke = model.Stroke,
                Gender = model.Gender,
                Lanes = laneList
            };

            eveRP.Add(eve);

            return CreatedAtRoute("DefaultApi", new { id = eve.EventId }, eve);
        }

        //// DELETE: api/Events/5
        //[ResponseType(typeof(Event))]
        //public IHttpActionResult DeleteEvent(int id)
        //{
        //    Event @event = db.Events.Find(id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Events.Remove(@event);
        //    db.SaveChanges();

        //    return Ok(@event);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool EventExists(int id)
        //{
        //    return eveRP.Count(id);
        //}
    }
}