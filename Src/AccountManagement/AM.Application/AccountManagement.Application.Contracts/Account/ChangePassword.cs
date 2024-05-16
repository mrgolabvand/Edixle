using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public class ChangePassword
    {
        public long Id { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(155, ErrorMessage = ValidationMessage.MaxLength)]
        public string RePassword { get; set; }

    }
}
