using E_Phone.BLL.DTOs.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.ValidationRules.Model
{
    public class CreateModelValidation : AbstractValidator<CreateModelDTO>
    {
        public CreateModelValidation()
        {
            RuleFor(create => create.ModelName)
                .NotEmpty().WithMessage("Model ismi yazılmadan geçilemez.")
                .MaximumLength(50).WithMessage("Model ismi en fazla 50 karekter olabilir");
        }
    }
}
