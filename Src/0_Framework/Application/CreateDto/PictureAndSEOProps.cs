using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Application.CreateDto
{
    public abstract class PictureAndSEOProps : PictureProps
    {
        [MaxLength(400, ErrorMessage = ValidationMessage.MaxLength)]
        public string Slug { get; set; }
        
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string Keywords { get; set; }
        
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string MetaDescription { get; set; }
    }
}
