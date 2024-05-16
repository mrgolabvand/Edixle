using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using _0_Framework.Application.CreateDto;
using AccountManagement.Application.Contracts.PortfolioAndCategoryLinked;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Portfolio
{
    public class CreatePortfolio : PictureAndSEOProps
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        public string Name { get; set; }

        [MaxLength(2000, ErrorMessage = ValidationMessage.MaxLength)]
        public string ShortDescription { get; set; }
        
        [MaxLength(3000, ErrorMessage = ValidationMessage.MaxLength)]
        public string Description { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [FileExtensionLimitation(new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [TemplateExtensionLimitation(new string[] { ".mp4", ".mkv", "wmv" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        public IFormFile Video { get; set; }

        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string Tags { get; set; }
        public List<AddPortfolioAndCategoryLink> AddPortfolioAndCategoryLinks { get; set; }
        public long PageId { get; set; }
    }
}
