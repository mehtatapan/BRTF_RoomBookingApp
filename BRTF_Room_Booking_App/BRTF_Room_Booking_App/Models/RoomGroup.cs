using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Room_Booking_App.Models
{
    public class RoomGroup
    {
        public RoomGroup()
        {
            this.Rooms = new HashSet<Room>();
            this.RoomUserGroupPermissions = new HashSet<RoomUserGroupPermission>();
        }

        public int ID { get; set; }

        [Display(Name = "Area")]
        [Required(ErrorMessage = "Cannot be blank.")]
        [StringLength(100, ErrorMessage = "Cannot be more than 100 characters long.")]
        public string AreaName { get; set; }

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "Cannot be more than 1000 characters long.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Hours of Blackout Time Between Consecutive Bookings in This Area")]
        [Required(ErrorMessage = "Cannot be blank.")]
        public int BlackoutTime { get; set; }

        [Display(Name = "Maximum Hours Per Single Booking in This Area")]
        public int? MaxHoursPerSingleBooking { get; set; }

        [Display(Name = "Maximum Total Hours That Can Be Booked in This Area")]
        public int? MaxHoursTotal { get; set; }

        [Display(Name = "Maximum Number of Separate Bookings in This Area")]
        public int? MaxNumberOfBookings { get; set; }

        [Display(Name = "Area is Enabled")]
        public bool Enabled { get; set; }

        [Display(Name = "Rooms")]
        public ICollection<Room> Rooms { get; set; }

        [Display(Name = "Room User Group Permissions")]
        public ICollection<RoomUserGroupPermission> RoomUserGroupPermissions { get; set; }
    }
}
