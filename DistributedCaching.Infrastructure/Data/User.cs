using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DistributedCaching.Infrastructure.Data
{
   public class User
    {
        [Key]
        public Guid Id { get; set; }
      //  [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

       // [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

//[Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
    }
}
