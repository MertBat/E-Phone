using E_Phone.BLL.DTOs.Version;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.ValidationRules.Version
{
    public class CreateVersionValidation : AbstractValidator<CreateVersionDTO>
    {
        public CreateVersionValidation()
        {
            RuleFor(create => create.price)
                .NotEmpty().WithMessage("Fiyat boş olamaz")
                .GreaterThan(0).WithMessage("Fiyat 0 dan büyük olmalıdır.");

            RuleFor(create => create.Stock)
                .NotEmpty().WithMessage("Stok boş olamaz")
                .GreaterThan(0).WithMessage("Stok 0 dan büyük olmalıdır.");

            RuleFor(create => create.StorageCapacity)
                .NotEmpty().WithMessage("Depo kapesitesi boş olamaz")
                .GreaterThan(0).WithMessage("Depo kapasitesi 0 dan büyük olmalıdır.")
                .GreaterThanOrEqualTo(create => create.Stock).WithMessage("Stok kapesitesi stoktan daha düşük olamaz");
        }
    }
}
