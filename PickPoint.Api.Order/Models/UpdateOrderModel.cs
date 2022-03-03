namespace PickPoint.Api.Order.Models
{
    public class UpdateOrderModel
    {
        public string[] Articles { get; set; }

        public decimal Price { get; set; }

        public string RecipientPhone { get; set; }

        public string RecipientFullName { get; set; }
    }
}
