using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Room_Booking_App.Models
{
    public class User
    {
        public int ID { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Cannot be blank.")]
        [StringLength(50, ErrorMessage = "Cannot be more than 50 characters long.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Cannot be blank.")]
        [StringLength(50, ErrorMessage = "Cannot be more than 50 characters long.")]
        public string Password { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Cannot be blank.")]
        [StringLength(100, ErrorMessage = "Cannot be more than 100 characters long.")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [StringLength(200, ErrorMessage = "Cannot be more than 200 characters long.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Receive email notifications when I make a booking")]
        public bool EmailBookingNotifications { get; set; }

        [Display(Name = "Receive email notifications when my booking is cancelled")]
        public bool EmailCancelNotifications { get; set; }

        [Display(Name = "Term and Program")]
        [Required(ErrorMessage = "You must assign a Term and Program.")]
        public int TermAndProgramID { get; set; }

        [Display(Name = "Term and Program")]
        public TermAndProgram TermAndProgram { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "You must assign a Role.")]
        public int RoleID { get; set; }

        [Display(Name = "Role")]
        public Role Role { get; set; }
    }
}
