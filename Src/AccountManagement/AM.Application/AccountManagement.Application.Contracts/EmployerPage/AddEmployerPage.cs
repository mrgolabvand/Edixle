using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.EmployerPage
{
    public class AddEmployerPage
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string FullName { get; set; }


        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessage.MaxLength)]
        public string Card { get; set; }

        //[FileExtensionLimitation(new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Picture { get; set; }

        public long AccountId { get; set; }
    }
}
