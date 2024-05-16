using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EdixleQuery.Contracts.TextSlider;

namespace ServiceHost.ViewComponents
{
    public class HeroViewComponent : ViewComponent
    {
        private readonly ITextSliderQuery _textSliderQuery;

        public HeroViewComponent(ITextSliderQuery textSliderQuery)
        {
            _textSliderQuery = textSliderQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _textSliderQuery.GetListAsync();
            return View("Hero", sliders);
        }

    }
}
