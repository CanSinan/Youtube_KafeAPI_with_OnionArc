using FluentValidation;
using KafeAPI.Application.Dtos.MenuItemDtos;

namespace KafeAPI.Application.Validators.MenuItem
{
    public class CreateMenuItemValidator : AbstractValidator<CreateMenuItemDto>
    {
        public CreateMenuItemValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Menü Item adı boş olamaz.")
                .Length(2, 40).WithMessage("Menü Item adı 3 ile 40 karakter arasında olmalıdır.");
            RuleFor(m => m.Description)
                .NotEmpty().WithMessage("Menü Item açıklaması boş olamaz.")
                .Length(5, 100).WithMessage("Menü Item açıklaması 5 ile 100 karakter arası olmalıdır.");
            RuleFor(m => m.Price)
                .NotEmpty().WithMessage("Menü Item fiyatı boş olamaz.")
                .GreaterThan(0).WithMessage("Menü Item fiyatı 0 dan büyük olmalıdır.");
            RuleFor(m => m.ImageUrl)
                .NotEmpty().WithMessage("Menü Item fotoğraf url i boş olamaz.");
        }
    }
}
