using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Room_Booking_App.Models
{
    public class Role
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }

        public int ID { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Cannot be blank.")]
        [StringLength(20, ErrorMessage = "Cannot be more than 20 characters long.")]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
