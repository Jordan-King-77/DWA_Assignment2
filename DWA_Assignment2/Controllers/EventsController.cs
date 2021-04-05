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

        // POST: api/Events
        [Authorize(Roles = "Club Official")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult PostEvent(EventViewModel model)
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
    }
}