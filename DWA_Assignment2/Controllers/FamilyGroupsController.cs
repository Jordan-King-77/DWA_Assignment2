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
    //[Authorize(Roles = "Club Official")]
    [RoutePrefix("api/FamilyGroups")]
    public class FamilyGroupsController : ApiController
    {
        private IRepository<FamilyGroup, SearchFamilyGroupViewModel> famGroupRP;

        public FamilyGroupsController()
        {
            famGroupRP = new FamilyGroupRepository();
        }

        public FamilyGroupsController(IRepository<FamilyGroup, SearchFamilyGroupViewModel> repository)
        {
            famGroupRP = repository;
        }

        // GET: api/FamilyGroups
        [Route("FamilyGroups")]
        [Authorize(Roles = "Club Official")]
        public List<FamilyGroup> GetFamilyGroups()
        {
            return famGroupRP.ToList();
        }

        // GET: api/FamilyGroups/5
        [Route("FamilyGroup")]
        [Authorize(Roles = "Club Official")]
        [ResponseType(typeof(FamilyGroup))]
        public IHttpActionResult GetFamilyGroup(int id)
        {
            FamilyGroup familyGroup = famGroupRP.Find(id);

            if (familyGroup == null)
            {
                return NotFound();
            }

            return Ok(familyGroup);
        }

        // PUT: api/FamilyGroups/5
        [Route("FamilyGroup")]
        [Authorize(Roles = "Parent")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFamilyGroup(/*int id, */UpdateFamilyGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var manager = famGroupRP.CreateUserStore();
            string userId = RequestContext.Principal.Identity.GetUserId();

            var user = manager.FindById(userId);

            if(user == null)
            {
                return NotFound();
            }

            var group = famGroupRP.Find(user.FamilyGroupId);

            if(group == null)
            {
                return NotFound();
            }

            if (model.GroupName != "")
            {
                group.GroupName = model.GroupName;
            }
            if (model.PhoneNumber != "")
            {
                group.PhoneNumber = model.PhoneNumber;
            }

            famGroupRP.Update(group);

            return Ok();
        }

        // POST: api/FamilyGroups
        [Route("FamilyGroups")]
        [Authorize(Roles = "Club Official")]
        [ResponseType(typeof(FamilyGroup))]
        public IHttpActionResult PostFamilyGroup(FamilyGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<ApplicationUser> swimmerList = new List<ApplicationUser>();
            ApplicationUser child1 = null;
            ApplicationUser child2 = null;
            ApplicationUser child3 = null;
            ApplicationUser child4 = null;
            ApplicationUser child5 = null;

            var manager = famGroupRP.CreateUserStore();

            var parent = manager.FindByEmail(model.ParentEmail);

            if (parent == null)
            {
                return NotFound();
            }

            child1 = manager.FindByEmail(model.Child1Email);

            if (child1 == null)
            {
                return NotFound();
            }
            else
            {
                swimmerList.Add(child1);
            }

            child2 = IsChildValid(manager, model.Child2Email);
            if(child2 != null) { swimmerList.Add(child2); }

            child3 = IsChildValid(manager, model.Child3Email);
            if (child3 != null) { swimmerList.Add(child3); }

            child4 = IsChildValid(manager, model.Child4Email);
            if (child4 != null) { swimmerList.Add(child4); }

            child5 = IsChildValid(manager, model.Child5Email);
            if (child5 != null) { swimmerList.Add(child5); }

            FamilyGroup group = new FamilyGroup
            {
                GroupName = model.GroupName,
                Email = model.ParentEmail,
                PhoneNumber = model.PhoneNumber,
                Parent = parent,
                Swimmers = swimmerList
            };

            famGroupRP.Add(group);

            group.Parent.FamilyGroupId = group.GroupId;
            manager.Update(group.Parent);

            foreach(var child in group.Swimmers)
            {
                child.FamilyGroupId = group.GroupId;
                manager.Update(child);
            }
            
            return CreatedAtRoute("DefaultApi", new { id = group.GroupId }, group);
        }

        //GET: api/FamilyGroups/MyFamilyGroup
        [Route("MyFamilyGroup")]
        [Authorize(Roles = "Parent,Swimmer")]
        [ResponseType(typeof(FamilyGroup))]
        public IHttpActionResult GetMyFamilyGroup()
        {
            var manager = famGroupRP.CreateUserStore();
            string userId = RequestContext.Principal.Identity.GetUserId();

            var user = manager.FindById(userId);

            if (user == null)
            {
                return NotFound();
            }

            var familyGroup = famGroupRP.Find(user.FamilyGroupId);

            if(familyGroup == null)
            {
                return NotFound();
            }

            return Ok(familyGroup);
        }

        //// DELETE: api/FamilyGroups/5
        //[ResponseType(typeof(FamilyGroup))]
        //public IHttpActionResult DeleteFamilyGroup(int id)
        //{
        //    FamilyGroup familyGroup = db.FamilyGroups.Find(id);
        //    if (familyGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    db.FamilyGroups.Remove(familyGroup);
        //    db.SaveChanges();

        //    return Ok(familyGroup);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool FamilyGroupExists(int id)
        {
            return famGroupRP.Count(id);
        }
        
        private ApplicationUser IsChildValid(UserManager<ApplicationUser> manager, string email)
        {
            if (email != null)
            {
                var child = manager.FindByEmail(email);

                if (child != null)
                {
                    return child;
                }
            }

            return null;
        }
    }
}