using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class EditArticleCategory
    {
        public long Id { get; set; }
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(3000, ErrorMessage = ValidationMessage.MaxLength)]
        public string Description { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        public int ShowOrder { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(600, ErrorMessage = ValidationMessage.MaxLength)]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessage.MaxLength)]
        public string Keyeords { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(150, ErrorMessage = ValidationMessage.MaxLength)]
        public string MetaDescription { get; set; }

        [MaxLength(1000, ErrorMessage = ValidationMessage.MaxLength)]
        public string CanonicalAddress { get; set; }
    }
}
