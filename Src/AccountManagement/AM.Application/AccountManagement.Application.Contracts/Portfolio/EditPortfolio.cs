using System;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Portfolio
{
    public class EditPortfolio
    {
        public long Id { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        public string Name { get; set; }

        [MaxLength(2000, ErrorMessage = ValidationMessage.MaxLength)]
        public string ShortDescription { get; set; }

        [MaxLength(3000, ErrorMessage = ValidationMessage.MaxLength)]
        public string Description { get; set; }

        [TemplateExtensionLimitation(new string[] { ".mp4", ".mkv", ".wav" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        //[MaxTemplateSize(100 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Video { get; set; }

        [FileExtensionLimitation(new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Picture { get; set; }

        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureAlt { get; set; }
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureTitle { get; set; }

        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string Tags { get; set; }
        [MaxLength(400, ErrorMessage = ValidationMessage.MaxLength)]
        public string Slug { get; set; }
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string Keywords { get; set; }
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string MetaDescription { get; set; }
        public long PageId { get; set; }
    }
}