using E_Phone.BLL.DTOs.Order;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.ValidationRules.Order
{
    public class CreateOrderValidation : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderValidation()
        {
            RuleFor(create => create.VersionId)
                .NotEmpty().WithMessage("Id boş olamaz")
                .GreaterThan(0).WithMessage("ID 0 dan büyük olmalıdır");

            RuleFor(create => create.OrderCount)
                .NotEmpty().WithMessage("Sipariş miktarı boş olamaz")
                .GreaterThan(0).WithMessage("Sipariş miktarı 0 dan büyük olmalıdır");
        }
    }
}
