using E_Phone.BLL.DTOs.Version;
using E_Phone.BLL.ValidationRules.Auth;
using E_Phone.BLL.ValidationRules.Brand;
using E_Phone.BLL.ValidationRules.Model;
using E_Phone.BLL.ValidationRules.Order;
using E_Phone.BLL.ValidationRules.Version;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL
{
    public static class ValidationRegistrations
    {
        public static void ValidationControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidation>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterValidation>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateValidation>())

                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateBrandValidation>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateBrandValidation>())

                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateBrandValidation>())
                .AddFluentValidation(fv=> fv.RegisterValidatorsFromAssemblyContaining<UpdateBrandValidation>())

                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateModelValidation>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateModelValidation>())

                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateVersionValidation>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateVersionValidation>())

                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateOrderValidation>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateOrderValidation>());

        }
    }
}
