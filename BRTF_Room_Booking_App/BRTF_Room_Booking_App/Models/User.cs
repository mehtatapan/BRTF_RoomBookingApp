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

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot be more than 50 characters long.")]
        public string Username { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle Name cannot be more than 50 characters long.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last Name cannot be more than 100 characters long.")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(200, ErrorMessage = "Email cannot be more than 200 characters long.")]
        [RegularExpression("^[a-zA-Z]+?[a-zA-Z0-9]*?@[a-zA-Z.]*?niagaracollege\\.ca$", ErrorMessage = "Email must be a Niagara College email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Receive email confirmation when I make a booking")]
        public bool EmailBookingNotifications { get; set; }

        [Display(Name = "Receive email when my booking is cancelled")]
        public bool EmailCancelNotifications { get; set; }

        [Display(Name = "Term and Program")]
        [Required(ErrorMessage = "You must assign a Term and Program.")]
        public int TermAndProgramID { get; set; }

        [Display(Name = "Term and Program")]
        public TermAndProgram TermAndProgram { get; set; }
    }
}
