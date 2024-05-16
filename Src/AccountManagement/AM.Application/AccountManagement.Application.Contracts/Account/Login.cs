using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public class Login
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(155, ErrorMessage = ValidationMessage.MaxLength)]
        public string Password { get; set; }
    }
}
