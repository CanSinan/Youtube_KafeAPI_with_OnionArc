using FluentValidation;
using KafeAPI.Application.Dtos.TableDtos;

namespace KafeAPI.Application.Validators.Table
{
    public class UpdateTableValidator : AbstractValidator<UpdateTableDto>
    {
        public UpdateTableValidator()
        {
            RuleFor(x => x.TableNumber)
             .NotEmpty().WithMessage("Masa numarası boş olamaz")
             .GreaterThan(0).WithMessage("Masa numarası 0'dan büyük olmalıdır");
            RuleFor(x => x.Capacity)
           .NotEmpty().WithMessage("Kapasite boş olamaz")
           .GreaterThan(0).WithMessage("Kapasite 0'dan büyük olmalıdır");
        }
    }
}
