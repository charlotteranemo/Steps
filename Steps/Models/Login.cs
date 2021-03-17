using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Steps.Models
{
    public class Login
    {
        public int LoginId { get; set; }
        [Required(ErrorMessage = "You need to enter your username")]
        public string Username { get; set; }
        [DataType("Password")]
        [Required(ErrorMessage = "You need to enter your password")]
        public string Password { get; set; }
    }
}
