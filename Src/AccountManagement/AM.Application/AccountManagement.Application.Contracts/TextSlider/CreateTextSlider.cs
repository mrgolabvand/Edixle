using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.TextSlider
{
    public class CreateTextSlider
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessage.MaxLength)]
        public string Title { get; set; }
        
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(300, ErrorMessage = ValidationMessage.MaxLength)]
        public string Description { get; set; }
        
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string Link { get; set; }
    }
}
