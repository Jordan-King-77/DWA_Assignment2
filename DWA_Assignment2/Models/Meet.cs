using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DWA_Assignment2.Models
{
    public class Meet
    {
        [Key, Column(Order = 0)]
        public int MeetId { get; set; }
        [StringLength(20)]
        public string MeetName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public string PoolLength { get; set; }
        public virtual List<Event> Events { get; set; }
    }
}