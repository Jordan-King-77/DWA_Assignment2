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

    public class MeetViewModel : IValidatableObject
    {
        [JsonIgnore]
        [Display(Name = "Meet Id")]
        public int MeetId { get; set; }

        [Required(ErrorMessage = "The name of the meet is required")]
        [Display(Name = "Meet Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The date of the meet is required")]
        [Display(Name = "Meet Date")]
        public string DateString { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Provided date could not be parsed. Please try another format")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The venue of the meet is required")]
        [Display(Name = "Meet Venue")]
        public string Venue { get; set; }

        [Required(ErrorMessage = "The pool length is required")]
        [Display(Name = "Pool Length")]
        public string PoolLength { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateTime d;
            if (!DateTime.TryParse(DateString, out d))
            {
                yield return new ValidationResult("Date format could not be parsed");
            }
            else
            {
                Date = d;
            }
        }
    }


    //public class LaneViewModel : IValidatableObject
    //{
    //    [Display(Name = "Lane Id")]
    //    public int EventId { get; set; }

    //    [Required]

    //    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}