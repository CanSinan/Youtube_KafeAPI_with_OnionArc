using FluentValidation;
using KafeAPI.Application.Dtos.ReviewDtos;

namespace KafeAPI.Application.Validators.Review
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewValidator()
        {
            RuleFor(RuleFor => RuleFor.Comment)
              .NotEmpty()
              .WithMessage("Yorum boş olamaz.")
              .Length(5, 500)
              .WithMessage("Yorum alanı en az 5 karakter, en fazla 500 karakter olabilir.");

            RuleFor(RuleFor => RuleFor.Raiting)
                .NotNull()
                .WithMessage("Yıldız değeri boş olamaz.")
                .InclusiveBetween(1, 5)
                .WithMessage("Yıldız değeri 1 ve 5 arası olmalıdır.");
        }
    }
}
