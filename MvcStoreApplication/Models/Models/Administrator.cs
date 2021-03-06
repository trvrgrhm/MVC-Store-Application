﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Administrator : IUser
    {
        [Key]
        public Guid UserId { get ; set ; }

        [StringLength(20, MinimumLength = 5, ErrorMessage = "The username must be between 5 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Use letters and numbers only please")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The password must be between 8 and 40 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Please include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(20, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [StringLength(20, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        public AdminAccessLevel Acesslevel { get; set; }
    }
}
