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
            var model =
                from m in context.Lanes
                orderby m.LaneId descending
                where (Search.SwimmerId == null || m.Swimmer.Id == Search.SwimmerId)
                where (Search.FirstName == null || m.Swimmer.FirstName.StartsWith(Search.FirstName))
                where (Search.LastName == null || m.Swimmer.LastName.StartsWith(Search.LastName))
                where (Search.DOBStartDateDT == null && Search.DOBEndDateDT == null || m.Swimmer.DateOfBirth > Search.DOBStartDateDT && m.Swimmer.DateOfBirth < Search.DOBEndDateDT)
                select m;

            return model;
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