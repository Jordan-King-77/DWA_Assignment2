using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DWA_Assignment2.Models
{
    public class MeetRepository : IRepository<Meet, SearchMeetViewModel>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Meet entity)
        {
            context.Meets.Add(entity);
            context.SaveChanges();
        }

        public bool Count(int? id)
        {
            return context.Meets.Count(e => e.MeetId == id) > 0;
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

        public Meet Find(int? Id)
        {
            return context.Meets.Find(Id);
        }

        public IEnumerable<Meet> Search(SearchMeetViewModel Search)
        {
            var model =
                from m in context.Meets
                orderby m.MeetName descending
                where (Search.Name == null || m.MeetName.StartsWith(Search.Name))
                where (Search.Venue == null || m.Venue.StartsWith(Search.Venue))
                where (Search.StartDateDT == null && Search.EndDateDT == null || m.Date > Search.StartDateDT && m.Date < Search.EndDateDT)
                where (Search.SwimmerId == null || m.Events.Any(e => e.Lanes.Any(l => l.Swimmer.Id == Search.SwimmerId)))
                select m;

            return model;
        }

        public List<Meet> ToList()
        {
            return context.Meets.ToList();
        }

        public void Update(Meet entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}