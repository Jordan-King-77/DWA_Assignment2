using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
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

    public class EventViewModel : IValidatableObject
    {
        [JsonIgnore]
        [Display(Name = "Event Id")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "The Id of the meet this event is for is required")]
        [Display(Name = "Meet Id")]
        public int? MeetId { get; set; }

        [Required(ErrorMessage = "The age range is required")]
        [Display(Name = "Age Range")]
        public string AgeRange { get; set; }

        [Required(ErrorMessage = "The gender is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "The distance is required")]
        [Display(Name = "Distance")]
        public string Distance { get; set; }

        [Required(ErrorMessage = "The stroke is required")]
        [Display(Name = "Stroke")]
        public string Stroke { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IRepository<Meet> meetRP = new MeetRepository();

            var meet = meetRP.Find(MeetId);

            if (meet == null)
            {
                yield return new ValidationResult("The meet Id " + MeetId + " is not a valid meet");
            }

            meetRP.Dispose();
        }
    }


    public class LaneViewModel : IValidatableObject
    {
        [JsonIgnore]
        [DataMember(Name = "Lane Id")]
        public int LaneId { get; set; }

        [Required(ErrorMessage = "The Event Id is required")]
        [DataMember(Name = "Event Id")]
        public int? EventId { get; set; }

        [Required(ErrorMessage = "Lane Number is required")]
        [DataMember(Name = "Lane Number")]
        public int LaneNumber { get; set; }

        [Required(ErrorMessage = "Swimmer's email is required")]
        [DataMember(Name = "Swimmer's email")]
        public string SwimmerEmail { get; set; }

        [DataMember(Name = "Swimmer Time")]
        public string SwimmerTime { get; set; } = null;

        [JsonIgnore]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "The Heat is required")]
        [DataMember(Name = "Heat")]
        public string Heat { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IRepository<Event> eveRP = new EventRepository();
            var manager = eveRP.CreateUserStore();

            var eve = eveRP.Find(EventId);

            if (eve == null)
            {
                yield return new ValidationResult("This event does not exist");
            }

            if (eve != null)
            {
                var swimmer = manager.FindByEmail(SwimmerEmail);

                if (swimmer == null)
                {
                    yield return new ValidationResult("No swimmer was found with this email");
                }

                if (swimmer != null)
                {
                    DateTime now = DateTime.Today;
                    int age = now.Year - swimmer.DateOfBirth.Year;

                    if (eve.AgeRange == "Junior" && age > 14)
                    {
                        yield return new ValidationResult("Swimmer is over 14");
                    }

                    if (eve.AgeRange == "Senior" && age < 15)
                    {
                        yield return new ValidationResult("Swimmer is under 15");
                    }

                    if (eve.AgeRange == "Senior" && age > 16)
                    {
                        yield return new ValidationResult("Swimmer is over 16");
                    }

                    if (eve.Gender == "Male" && swimmer.Gender == "Female")
                    {
                        yield return new ValidationResult("This event requires: " + eve.Gender + " participants. Swimmer is " + swimmer.Gender);
                    }

                    if (eve.Gender == "Female" && swimmer.Gender == "Male")
                    {
                        yield return new ValidationResult("This event requires: " + eve.Gender + " participants. Swimmer is " + swimmer.Gender);
                    }
                }

                TimeSpan laneTime;

                if (!TimeSpan.TryParseExact(SwimmerTime, @"mm\:ss\.ff", null, out laneTime))
                {
                    Time = new TimeSpan(23, 59, 59);
                }
                else
                {
                    Time = laneTime;
                }
            }

            eveRP.Dispose();
        }
    }

    public class UpdateSwimmerTimeViewModel : IValidatableObject
    {
        [Display(Name = "Swimmer Time")]
        [Required(ErrorMessage = "Please enter the swimmer's time")]
        public string SwimmerTimeString { get; set; }
        [JsonIgnore]
        public TimeSpan SwimmerTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            TimeSpan laneTime;

            if (!TimeSpan.TryParseExact(SwimmerTimeString, @"mm\:ss\.ff", null, out laneTime))
            {
                yield return new ValidationResult("Unable to parse the entered time. Please use the format mm/ss/ff");
            }
            else
            {
                SwimmerTime = laneTime;
            }
        }
    }
}