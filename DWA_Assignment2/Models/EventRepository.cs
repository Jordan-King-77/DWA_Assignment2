using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DWA_Assignment2.Models
{
    public class EventRepository : IRepository<Event>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Event entity)
        {
            context.Events.Add(entity);
            context.SaveChanges();
        }

        public bool Count(int? id)
        {
            return context.Events.Count(e => e.EventId == id) > 0;
        }

        public UserManager<ApplicationUser> CreateUserStore()
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            return manager;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Event Find(int? Id)
        {
            return context.Events.Find(Id);
        }

        public List<Event> ToList()
        {
            return context.Events.ToList();
        }

        public void Update(Event entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}