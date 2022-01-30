using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Room_Booking_App.Models
{
    public class RoomBooking
    {
        public int ID { get; set; }

        [Display(Name = "Special Notes")]
        [StringLength(1000, ErrorMessage = "Cannot be more than 1000 characters long.")]
        [DataType(DataType.MultilineText)]
        public string SpecialNotes { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Cannot be blank.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = "You must assign an Room.")]
        public int RoomID { get; set; }

        [Display(Name = "Room")]
        public Room Room { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "You must assign a User.")]
        public int UserID { get; set; }

        [Display(Name = "User")]
        public User User { get; set; }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "You must assign a Start Time.")]
        public int StartTimeID { get; set; }

        [Display(Name = "Start Time")]
        public BookingTime StartTime { get; set; }

        [Display(Name = "End Time")]
        [Required(ErrorMessage = "You must assign a End Time.")]
        public int EndTimeID { get; set; }

        [Display(Name = "End Time")]
        public BookingTime EndTime { get; set; }
    }
}
