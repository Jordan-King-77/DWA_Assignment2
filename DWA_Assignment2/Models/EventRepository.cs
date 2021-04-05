using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DWA_Assignment2.Models
{
    public class EventRepository : IRepository<Event, SearchEventViewModel>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Event entity)
        {
            context.Events.Add(entity);
            context.SaveChanges();
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

        public IEnumerable<Event> Search(SearchEventViewModel Search)
        {
            var model =
                from m in context.Events
                orderby m.EventId descending
                where (Search.SwimmerId == null || m.Lanes.Any(l => l.Swimmer.Id == Search.SwimmerId))
                where (Search.AgeRange == null || m.AgeRange == Search.AgeRange)
                where (Search.Gender == null || m.Gender == Search.Gender)
                where (Search.Distance == null || m.Distance.StartsWith(Search.Distance))
                where (Search.SwimStroke == null || m.Stroke.StartsWith(Search.SwimStroke))
                select m;

            return model;
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