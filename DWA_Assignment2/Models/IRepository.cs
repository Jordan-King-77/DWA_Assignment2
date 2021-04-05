using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DWA_Assignment2.Models
{
    public interface IRepository<T, S> where T : class, new() where S : class
    {
        void Add(T entity);
        void Dispose();
        T Find(int? Id);
        List<T> ToList();
        void Update(T entity);
        UserManager<ApplicationUser> CreateUserStore();
        IEnumerable<T> Search(S Search);
    }
}