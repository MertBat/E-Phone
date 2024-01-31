using E_Phone.BLL.DTOs.Order;
using E_Phone.Core.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.ValidationRules.Order
{
    public class UpdateOrderValidation : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderValidation()
        {
            RuleFor(update => update.VersionId)
                .NotEmpty().WithMessage("Id boş olamaz")
                .GreaterThan(0).WithMessage("ID 0 dan büyük olmalıdır");

            RuleFor(update => update.OrderCount)
                .NotEmpty().WithMessage("Sipariş miktarı boş olamaz")
                .GreaterThan(0).WithMessage("Sipariş miktarı 0 dan büyük olmalıdır");

            RuleFor(update => update.OrderCondition)
                .NotEmpty().WithMessage("Sipariş durumu boş olamaz")
                .Must(condition => Enum.IsDefined(typeof(OrderCondition), condition)).WithMessage("sipariş durumu 0, 1 veya 2 olabilir. 0 => Beklemekte, 1 => Tamamlandı, 2 => İptal edildi");
        }
    }
}
