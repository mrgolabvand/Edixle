using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application.CreateDto
{
    public abstract class PictureProps
    {
        [FileExtensionLimitation(new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Picture { get; set; }

        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureAlt { get; set; }
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string PictureTitle { get; set; }
    }
}
