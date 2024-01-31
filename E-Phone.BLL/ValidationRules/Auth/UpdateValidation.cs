using E_Phone.BLL.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.ValidationRules.Auth
{
    public class UpdateValidation : AbstractValidator<UpdateUserDTO>
    {
        public UpdateValidation()
        {
            RuleFor(update => update.Email)
                .NotEmpty().WithMessage("E-mail boş geçilemez.")
                .EmailAddress().WithMessage("E-mail formatı yanlış.");

            RuleFor(update => update.Password)
                .NotEmpty().WithMessage("Şifre boş geçilemez.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter içermelidir.");

            RuleFor(update => update.NewPassword)
                .NotEmpty().WithMessage("Şifre boş geçilemez.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter içermelidir.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");

            RuleFor(update => update.ConfirmNewPassword)
                .NotEmpty().WithMessage("Yeni şifre tekrarı boş geçilemez.")
                .Equal(update => update.NewPassword).WithMessage("Yeni şifre ve onay şifresi eşleşmiyor");

            RuleFor(update => update.Surname)
                .NotEmpty().WithMessage("Soyad boş geçilemez.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            RuleFor(update => update.Name)
                .NotEmpty().WithMessage("İsim boş geçilemez.")
                .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olmalıdır.");
        }
    }
}
