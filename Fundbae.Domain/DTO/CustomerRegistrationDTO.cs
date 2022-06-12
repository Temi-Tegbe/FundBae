using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Domain.DTO
{
    public  class CustomerRegistrationDTO
    {
        [Required(ErrorMessage = "First Name is required."), MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required."), MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required."), MaxLength(150)]
        [EmailAddress(ErrorMessage = "This is not a valid email address.")]
        public string EmailAddress { get; set; }

        [MaxLength(20)]
        [Phone]
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }


        [Required]
        public DateTime DateRegistered { get; set; }

        public DateTime LastModified { get; set; }

        [Required(ErrorMessage = "Password is required."), MinLength(8), MaxLength(20)]
        public string Password { get; set; }
    }
}
