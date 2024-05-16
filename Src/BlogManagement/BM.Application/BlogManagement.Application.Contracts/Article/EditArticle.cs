using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.Article
{
    public class EditArticle
    {
        public long Id { get; set; }
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(1000, ErrorMessage = ValidationMessage.MaxLength)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(5000, ErrorMessage = ValidationMessage.MaxLength)]
        public string Description { get; set; }

        [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessage.MaxLength)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(150, ErrorMessage = ValidationMessage.MaxLength)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(600, ErrorMessage = ValidationMessage.MaxLength)]
        public string Slug { get; set; }

        [MaxLength(1000, ErrorMessage = ValidationMessage.MaxLength)]
        public string CanonicalAddress { get; set; }

        public string PublishDate { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.isRequired)]
        public long CategoryId { get; set; }
    }
}
