using FluentValidation;
using KafeAPI.Application.Dtos.CafeInfoDtos;

namespace KafeAPI.Application.Validators.CafeInfo
{
    public class UpdateCafeInfoValidator : AbstractValidator<UpdateCafeInfoDto>
    {
        public UpdateCafeInfoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kafe adı boş olamaz")
                .MaximumLength(100).WithMessage("Kafe adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş olamaz")
                .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş olamaz")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");
        }

    }
}
