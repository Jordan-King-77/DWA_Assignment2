using DWA_Assignment2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DWA_Assignment2.Controllers
{
    [RoutePrefix("api/Search")]
    public class SearchController : ApiController
    {
        private IRepository<Meet, SearchMeetViewModel> meetRP;
        private IRepository<Event, SearchEventViewModel> eveRP;
        private IRepository<Lane, SearchLanesViewModel> laneRP;

        public SearchController()
        {
            meetRP = new MeetRepository();
            eveRP = new EventRepository();
            laneRP = new LaneRepository();
        }

        public SearchController(IRepository<Meet, SearchMeetViewModel> repository)
        {
            meetRP = repository;
        }

        public SearchController(IRepository<Event, SearchEventViewModel> repository)
        {
            eveRP = repository;
        }

        public SearchController(IRepository<Lane, SearchLanesViewModel> repository)
        {
            laneRP = repository;
        }


        [Authorize(Roles = "Swimmer")]
        [Route("MyMeets")]
        public List<Meet> GetMyMeets()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            SearchMeetViewModel model = new SearchMeetViewModel
            {
                SwimmerId = userId
            };

            var meets = meetRP.Search(model);

            return meets.ToList();
        }

        [Authorize(Roles = "Swimmer")]
        [Route("MyEvents")]
        public List<Event> GetMyEvents()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            SearchEventViewModel model = new SearchEventViewModel
            {
                SwimmerId = userId
            };

            var events = eveRP.Search(model);

            return events.ToList();
        }

        [Authorize(Roles = "Swimmer")]
        [Route("MyResults")]        
        public List<Lane> GetMyResults()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            SearchLanesViewModel model = new SearchLanesViewModel
            {
                SwimmerId = userId
            };

            var lanes = laneRP.Search(model);

            return lanes.ToList();
        }

        [AllowAnonymous]
        [Route("SearchMeets")]
        public IHttpActionResult GetSearchMeets(SearchMeetViewModel model)
        {
            if(ModelState.IsValid)
            {
                var meets = meetRP.Search(model);

                return Ok(meets.ToList());
            }

            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [Route("SearchEvents")]
        public IHttpActionResult GetSearchEvents(SearchEventViewModel model)
        {
            if(ModelState.IsValid)
            {
                var events = eveRP.Search(model);

                return Ok(events.ToList());
            }

            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [Route("SearchSwimmers")]
        public IHttpActionResult GetSearchSwimmers(SearchLanesViewModel model)
        {
            if(ModelState.IsValid)
            {
                var swimmers = laneRP.Search(model);

                return Ok(swimmers.ToList());
            }

            return BadRequest(ModelState);
        }
    }
}
