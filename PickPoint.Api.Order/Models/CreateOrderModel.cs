using FluentValidation;

namespace PickPoint.Api.Order.Models
{
    public class CreateOrderModel
    {
        public string[] Articles { get; set; }

        public decimal Price { get; set; }

        public string ParcelLockNumber { get; set; }

        public string RecipientPhone { get; set; }

        public string RecipientFullName { get; set; }

    }

    public class CreateOrderModelValidator : AbstractValidator<CreateOrderModel>
    {
        public CreateOrderModelValidator()
        {
            RuleFor(x => x.Articles).NotNull().Must(x => x.Length < 10).WithErrorCode("400");
            RuleFor(x => x.RecipientPhone).Matches("^(\\+7)[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}").WithErrorCode("400");
            RuleFor(x => x.ParcelLockNumber).Matches("[0-9]{4}-[0-9]{3}").WithErrorCode("400");
        }
    }
}
