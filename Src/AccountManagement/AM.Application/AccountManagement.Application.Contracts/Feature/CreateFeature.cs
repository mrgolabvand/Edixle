using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Feature
{
    public class CreateFeature
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(300, ErrorMessage = ValidationMessage.MaxLength)]
        public string Title { get; set; }
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(700, ErrorMessage = ValidationMessage.MaxLength)]
        public string Description { get; set; }
        
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Picture { get; set; }

        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureAlt { get; set; }

        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureTitle { get; set; }
    }
}
