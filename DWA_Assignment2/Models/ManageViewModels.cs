using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DWA_Assignment2.Models
{
    public class FamilyGroupViewModel
    {
        [JsonIgnore]
        [Display(Name = "Group Id")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "The group name is required")]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "The parent email is required")]
        [Display(Name = "Parent Email")]
        public string ParentEmail { get; set; }

        [Required(ErrorMessage = "This child email is required")]
        [Display(Name = "Child 1 Email")]
        public string Child1Email { get; set; }

        [Display(Name = "Child 2 Email")]
        public string Child2Email { get; set; }

        [Display(Name = "Child 3 Email")]
        public string Child3Email { get; set; }

        [Display(Name = "Child 4 Email")]
        public string Child4Email { get; set; }

        [Display(Name = "Child 5 Email")]
        public string Child5Email { get; set; }

        [Required(ErrorMessage = "The phone number is required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class UpdateFamilyGroupViewModel
    {
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}