using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Skill
{
    public class AddSkill
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)] 
        public string Name { get; set; }
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(4, ErrorMessage = ValidationMessage.MaxLength)] 
        public string Value { get; set; }
        public long PageId { get; set; }
    }
}
