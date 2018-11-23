using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATTime.Models.ViewModel
{
    public class RegistorViewModel
    {

        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Psw { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string Logo { get; set; }
    }
}