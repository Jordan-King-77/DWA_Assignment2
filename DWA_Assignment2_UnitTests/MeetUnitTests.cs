using DWA_Assignment2.Controllers;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using DWA_Assignment2.Models;
using System.Web.Http.Results;
using System.Collections.Generic;
using System;

namespace DWA_Assignment2_UnitTests
{
    [TestClass]
    public class MeetUnitTests
    {
        [TestMethod]
        public void ReturnSpecificMeet_Ok()
        {
            var controller = new MeetsController(new FakeMeetRepository());
            //controller.Request = new HttpRequestMessage();
            //controller.Configuration = new HttpConfiguration();

            IHttpActionResult actionResult = controller.GetMeet(3);

            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Meet>));
        }

        [TestMethod]
        public void ReturnSpecificMeet_NotFound()
        {
            var controller = new MeetsController(new FakeMeetRepository(false));
            //controller.Request = new HttpRequestMessage();
            //controller.Configuration = new HttpConfiguration();

            IHttpActionResult actionResult = controller.GetMeet(-1);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        //[TestMethod]
        //public void ReturnSpecificMeet_NotFound()
        //{
        //    var controller = new MeetsController(new FakeMeetRepository());
        //    IHttpActionResult actionResult = controller.GetMeet(-1);

        //    if ((Type)actionResult == typeof(NotFoundResult))
        //    {
        //        // Assert pass
        //        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        //    }
        //    else
        //    {
        //        // Assert fail
        //    }
        //}

        [TestMethod]
        public void PostMeet_Valid()
        {
            var controller = new MeetsController(new FakeMeetRepository());
            //controller.Request = new HttpRequestMessage();
            //controller.Configuration = new HttpConfiguration();

            MeetViewModel m = new MeetViewModel
            {
                Name = "Unit Test Meet",
                Venue = "Unit Test Venue",
                PoolLength = "150m",
                DateString = "21 September 2021"
            };

            Assert.IsInstanceOfType(controller.PostMeet(m), typeof(CreatedAtRouteNegotiatedContentResult<Meet>));
        }

        [TestMethod]
        public void PostMeet_Invalid()
        {
            var controller = new MeetsController(new FakeMeetRepository());
            //controller.Request = new HttpRequestMessage();
            //controller.Configuration = new HttpConfiguration();

            controller.ModelState.AddModelError("Date", "Date format could not be parsed");

            MeetViewModel m = new MeetViewModel
            {
                DateString = ""
            };

            Assert.IsInstanceOfType(controller.PostMeet(m), typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void GetMeets()
        {
            var controller = new MeetsController(new FakeMeetRepository());
            //controller.Request = new HttpRequestMessage();
            //controller.Configuration = new HttpConfiguration();

            var result = controller.GetMeets();

            Assert.IsInstanceOfType(result, typeof(List<Meet>));
        }
    }
}
