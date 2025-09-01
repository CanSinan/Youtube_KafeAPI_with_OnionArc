using FluentValidation;
using KafeAPI.Application.Dtos.OrderDtos;

namespace KafeAPI.Application.Validators.Order
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x =>x.TotalPrice)
                .NotEmpty().WithMessage("Toplam fiyat boş olamaz.")
                .GreaterThan(0).WithMessage("Toplam fiyat 0'dan büyük olmalıdır.");
        }
    }
}
