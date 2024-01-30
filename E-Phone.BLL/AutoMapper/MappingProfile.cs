using AutoMapper;
using E_Phone.BLL.DTOs.Auth;
using E_Phone.BLL.DTOs.Brand;
using E_Phone.BLL.DTOs.Model;
using E_Phone.BLL.DTOs.Order;
using E_Phone.BLL.DTOs.Version;
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
            CreateMap<Brand, GetBrandsDTO>();
            CreateMap<CreateBrandDTO, Brand>();
            CreateMap<Brand, GetSingleBrandDTO>();
            CreateMap<UpdateBrandDTO, Brand>();

            CreateMap<Model, GetModelsDTO>();
            CreateMap<Model, GetSingleModelDTO>();

            CreateMap<Core.Entities.Version, GetVersionsDTO>();
            CreateMap<Core.Entities.Version, GetSingleVersionDTO>();

            CreateMap<RegisterDTO, User>();
            CreateMap<User, GetUserDTO>();

            CreateMap<Order, GetOrdersDTO>();
            CreateMap<Order, GetUserOrdersDTO>();
            CreateMap<Order, GetSingleOrderDTO>();
        }
    }
}
