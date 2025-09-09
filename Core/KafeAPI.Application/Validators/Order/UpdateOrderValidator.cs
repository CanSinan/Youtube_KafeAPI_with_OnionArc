using FluentValidation;
using KafeAPI.Application.Dtos.OrderDtos;

namespace KafeAPI.Application.Validators.Order
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            //RuleFor(x => x.TotalPrice)
            //   .NotEmpty().WithMessage("Toplam fiyat boş olamaz.")
            //   .GreaterThan(0).WithMessage("Toplam fiyat 0'dan büyük olmalıdır.");
        }
    }
}
