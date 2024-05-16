using _0_Framework.Infrastructure;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.AutoMapperProfiles
{
    public class BaseRepositoryProfile : Profile
    {
        public BaseRepositoryProfile()
        {
            CreateMap(typeof(EditModel<>), typeof(BaseModel<>));
            CreateMap(typeof(BaseModel<>), typeof(EditModel<>));
        }
    }
}
