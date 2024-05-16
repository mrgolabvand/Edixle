using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contracts.PortfolioAndCategoryLinked
{
    public class PortfolioAndCategoryLinkViewModel
    {
        public long PortfolioId { get; set; }
        public long CategoryId { get; set; }
    }
}
