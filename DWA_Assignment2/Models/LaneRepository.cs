using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DWA_Assignment2.Models
{
    public class LaneRepository : IRepository<Lane, SearchLanesViewModel>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Lane entity)
        {
            context.Lanes.Add(entity);
            context.SaveChanges();
        }

        public bool Count(int? id)
        {
            return context.Lanes.Count(e => e.LaneId == id) > 0;
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

        public Lane Find(int? Id)
        {
            return context.Lanes.Find(Id);
        }

        public IEnumerable<Lane> Search(SearchLanesViewModel Search)
        {
            throw new NotImplementedException();
        }

        public List<Lane> ToList()
        {
            return context.Lanes.ToList();
        }

        public void Update(Lane entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}