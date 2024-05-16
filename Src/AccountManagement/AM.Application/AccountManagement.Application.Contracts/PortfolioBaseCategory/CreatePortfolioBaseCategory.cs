using System;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using _0_Framework.Application.CreateDto;

namespace AccountManagement.Application.Contracts.PortfolioBaseCategory
{
    public class CreatePortfolioBaseCategory : PictureAndSEOProps
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(180, ErrorMessage = ValidationMessage.MaxLength)] 
        public string Name { get; set; }
    }
}
