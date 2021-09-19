using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    public class LoginDTO
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "usuario creado")]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Password creado")]
        public string Password { get; set; }
    }
}