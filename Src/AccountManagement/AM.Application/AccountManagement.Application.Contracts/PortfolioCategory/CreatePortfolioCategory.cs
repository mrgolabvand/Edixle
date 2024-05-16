using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using _0_Framework.Application.CreateDto;
using AccountManagement.Application.Contracts.PortfolioBaseCategory;

namespace AccountManagement.Application.Contracts.PortfolioCategory
{
    public class CreatePortfolioCategory : PictureAndSEOProps
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(180, ErrorMessage = ValidationMessage.MaxLength)] 
        public string Name { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = ValidationMessage.isRequired)]
        public long BaseCategoryId { get; set; }
        public List<PortfolioBaseCategoryViewModel> BaseCateogries { get; set; }
    }
}
