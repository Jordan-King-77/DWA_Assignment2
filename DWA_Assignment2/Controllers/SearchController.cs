using DWA_Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DWA_Assignment2.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Search")]
    public class SearchController : ApiController
    {
        private IRepository<Meet, SearchMeetViewModel> meetRP;

        public SearchController()
        {
            meetRP = new MeetRepository();
        }

        public SearchController(IRepository<Meet, SearchMeetViewModel> repository)
        {
            meetRP = repository;
        }


        [Authorize(Roles = "Swimmer")]
        [Route("MyMeets")]
        public IHttpActionResult GetMyMeets()
        {
            

            //ToDo: Implement the rest of the search features
            return Ok();
        }
    }
}
