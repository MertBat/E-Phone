using E_Phone.BLL.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.ValidationRules.Auth
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation() 
        {
            RuleFor(register=> register.Email)
                .NotEmpty().WithMessage("E-mail boş geçilemez.")
                .EmailAddress().WithMessage("E-mail formatı yanlış.");

            RuleFor(register => register.Password)
                .NotEmpty().WithMessage("Şifre boş geçilemez.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter içermelidir.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");

            RuleFor(register => register.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre tekrarı boş geçilemez.")
                .Equal(register => register.Password).WithMessage("Şifre ve onay şifresi eşleşmiyor");

            RuleFor(register => register.Surname)
            .NotEmpty().WithMessage("Soyad boş geçilemez.")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            RuleFor(register => register.Name)
                .NotEmpty().WithMessage("İsim boş geçilemez.")
                .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olmalıdır.");

        }

    }
}
