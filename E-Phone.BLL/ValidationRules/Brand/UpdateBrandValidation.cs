using E_Phone.BLL.DTOs.Brand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.ValidationRules.Brand
{
    public class UpdateBrandValidation : AbstractValidator<UpdateBrandDTO>
    {
        public UpdateBrandValidation()
        {
            RuleFor(update => update.BrandName)
                .NotEmpty().WithMessage("Marka ismi yazılmadan geçilemez.")
                .MaximumLength(50).WithMessage("Marka ismi en fazla 50 karekter olabilir");
        }
    }
}
