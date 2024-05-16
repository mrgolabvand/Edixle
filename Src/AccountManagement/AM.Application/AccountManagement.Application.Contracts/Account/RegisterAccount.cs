using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Application.Contracts.Account
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(355, ErrorMessage = ValidationMessage.MaxLength)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(11, ErrorMessage = ValidationMessage.MaxLength)]
        [MinLength(11, ErrorMessage = ValidationMessage.MinLength)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(4, ErrorMessage = ValidationMessage.MaxLength)]
        public string VerificationCode { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(155, ErrorMessage = ValidationMessage.MaxLength)]
        public string Password { get; set; }
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(155, ErrorMessage = ValidationMessage.MaxLength)]
        public string RepeatPassword { get; set; }
        public long RoleId { get; set; }
        public List<RoleViewModel> Roles { get; set; }

    }
}
