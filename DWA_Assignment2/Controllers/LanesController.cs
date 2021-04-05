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
using Microsoft.AspNet.Identity;

namespace DWA_Assignment2.Controllers
{
    public class LanesController : ApiController
    {
        private IRepository<Lane, SearchLanesViewModel> laneRP;

        public LanesController()
        {
            laneRP = new LaneRepository();
        }

        public LanesController(IRepository<Lane, SearchLanesViewModel> repository)
        {
            laneRP = repository;
        }

        // GET: api/Lanes
        public List<Lane> GetLanes()
        {
            return laneRP.ToList();
        }

        // GET: api/Lanes/5
        [ResponseType(typeof(Lane))]
        public IHttpActionResult GetLane(int id)
        {
            Lane lane = laneRP.Find(id);
            if (lane == null)
            {
                return NotFound();
            }

            return Ok(lane);
        }

        // PUT: api/Lanes/5
        [Authorize(Roles = "Club Official")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLane(int id, UpdateSwimmerTimeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lane = laneRP.Find(id);

            if(lane == null)
            {
                return NotFound();
            }

            lane.SwimmerTime = model.SwimmerTime;

            laneRP.Update(lane);

            return Ok();

            //if (id != lane.LaneId)
            //{
            //    return BadRequest();
            //}

            //db.Entry(lane).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!LaneExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Lanes
        [Authorize(Roles = "Club Official")]
        [ResponseType(typeof(Lane))]
        public IHttpActionResult PostLane(LaneViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manager = laneRP.CreateUserStore();

            var swimmer = manager.FindByEmail(model.SwimmerEmail);

            Lane lane = new Lane
            {
                EventId = model.EventId,
                LaneNumber = model.LaneNumber,
                Heat = model.Heat,
                Swimmer = swimmer,
                SwimmerTime = model.Time
            };

            laneRP.Add(lane);

            return CreatedAtRoute("DefaultApi", new { id = lane.LaneId }, lane);
        }

        //// DELETE: api/Lanes/5
        //[ResponseType(typeof(Lane))]
        //public IHttpActionResult DeleteLane(int id)
        //{
        //    Lane lane = db.Lanes.Find(id);
        //    if (lane == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Lanes.Remove(lane);
        //    db.SaveChanges();

        //    return Ok(lane);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool LaneExists(int id)
        //{
        //    return laneRP.Count(id);
        //}
    }
}