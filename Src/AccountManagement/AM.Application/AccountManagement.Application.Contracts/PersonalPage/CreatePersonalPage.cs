using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using _0_Framework.Application.CreateDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace AccountManagement.Application.Contracts.PersonalPage
{
    public class CreatePersonalPage : PictureAndSEOProps
    {

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessage.MaxLength)]
        public string FullName { get; set; }

        [MaxLength(2000, ErrorMessage = ValidationMessage.MaxLength)]
        public string About { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(3, ErrorMessage = ValidationMessage.MaxLength)]
        public string Age { get; set; }
        
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(12, ErrorMessage = ValidationMessage.MaxLength)]
        public string MinPay { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(12, ErrorMessage = ValidationMessage.MaxLength)]
        public string MaxPay { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessage.MaxLength)]
        public string PayDate { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessage.MaxLength)]
        public string Card { get; set; }
        public long AccountId { get; set; }
        
    }
}
