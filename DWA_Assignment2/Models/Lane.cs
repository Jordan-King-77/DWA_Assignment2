using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DWA_Assignment2.Models
{
    public class Lane
    {
        [Key, Column(Order = 0)]
        public int LaneId { get; set; }
        public int? EventId { get; set; }

        public int LaneNumber { get; set; }
        public virtual ApplicationUser Swimmer { get; set; }

        public TimeSpan SwimmerTime { get; set; }
        public string Heat { get; set; }
    }
}