using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Room_Booking_App.Models
{
    public class BookingTime
    {
        public BookingTime()
        {
            this.BookingsStartingWithThisTime = new HashSet<RoomBooking>();
            this.BookingsEndingWithThisTime = new HashSet<RoomBooking>();
        }

        public int ID { get; set; }

        [Display(Name = "Time")]
        public string TwelveHourTimeSummary
        {
            get
            {
                // Generate return string.
                string twelveHourTimeString = ":" + MilitaryTimeMinute.ToString("00") + " ";

                int hourMod = MilitaryTimeHour % 12;

                twelveHourTimeString = ((hourMod == 0) ? "12" : hourMod.ToString()) + twelveHourTimeString;

                twelveHourTimeString += ((12 <= MilitaryTimeHour && MilitaryTimeHour <= 23) ? "p.m." : "a.m.");

                if (MilitaryTimeHour == 0 && MilitaryTimeMinute == 0)
                    twelveHourTimeString += " (Start of day)";
                else if (MilitaryTimeHour == 24)
                    twelveHourTimeString += " (End of day)";

                // Example output: "12:30 a.m. (Start of day)", "11:30 p.m.", "12:30 a.m. (End of day)"
                return twelveHourTimeString;
            }
        }

        [Display(Name = "Hour (24-Hour Time)")]
        [Required(ErrorMessage = "Cannot be blank.")]
        [Range(0, 24, ErrorMessage = "Must be between 0 and 24.")]
        public int MilitaryTimeHour { get; set; }

        [Display(Name = "Minute")]
        [Required(ErrorMessage = "Cannot be blank.")]
        [Range(0, 59, ErrorMessage = "Must be between 0 and 59.")]
        public int MilitaryTimeMinute { get; set; }

        [Display(Name = "Bookings Starting With This Time")]
        public ICollection<RoomBooking> BookingsStartingWithThisTime { get; set; }

        [Display(Name = "Bookings Ending With This Time")]
        public ICollection<RoomBooking> BookingsEndingWithThisTime { get; set; }
    }
}
