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
    public class LanesController : ApiController
    {
        private IRepository<Lane> laneRP;

        public LanesController()
        {
            laneRP = new LaneRepository();
        }

        public LanesController(IRepository<Lane> repository)
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

        //// PUT: api/Lanes/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutLane(int id, Lane lane)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != lane.LaneId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(lane).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LaneExists(id))
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

        // POST: api/Lanes
        [ResponseType(typeof(Lane))]
        public IHttpActionResult PostLane(Lane lane)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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