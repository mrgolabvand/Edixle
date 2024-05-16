using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.JobHistory
{
    public class AddJobHistory
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)] 
        public string JobTitle { get; set; }
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)] 
        public string EmployerName { get; set; }
        
        [MaxLength(1000, ErrorMessage = ValidationMessage.MaxLength)] 
        public string Description { get; set; }
        public long PageId { get; set; }
    }
}
