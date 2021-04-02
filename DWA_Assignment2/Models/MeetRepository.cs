using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DWA_Assignment2.Models
{
    public class MeetRepository : IRepository<Meet>
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