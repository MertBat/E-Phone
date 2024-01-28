using AutoMapper;
using E_Phone.BLL.DTOs.Brand;
using E_Phone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetBrandsDTO, Brand>().ReverseMap();
            CreateMap<CreateBrandDTO, Brand>().ReverseMap();
            CreateMap<GetSingleBrandDTO, Brand>().ReverseMap();
            CreateMap<UpdateBrandDTO, Brand>().ReverseMap();
        }
    }
}
