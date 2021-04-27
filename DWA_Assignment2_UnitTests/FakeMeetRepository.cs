using DWA_Assignment2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWA_Assignment2_UnitTests
{
    class FakeMeetRepository : IRepository<Meet, SearchMeetViewModel>
    {
        public bool found;

        public FakeMeetRepository(bool found = true)
        {
            this.found = found;
        }

        public void Add(Meet entity)
        {
        }

        public UserManager<ApplicationUser> CreateUserStore()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Meet Find(int? Id)
        {
            if (found)
            {
                return new Meet();
            }
            return null;
        }

        public IEnumerable<Meet> Search(SearchMeetViewModel Search)
        {
            if (found)
            {
                return new List<Meet>();
            }
            return null;
        }

        public List<Meet> ToList()
        {
            return new List<Meet>();
        }

        public void Update(Meet entity)
        {
        }
    }
}
