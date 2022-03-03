namespace PickPoint.Api.Order.Models
{
    public class OrderInfoModel
    {
        public int Number { get; set; }

        public int Status { get; set; }

        public string[] Articles { get; set; }

        public decimal Price { get; set; }

        public string ParcelLockerNumber { get; set; }

        public string RecipientPhone { get; set; }

        public string RecipientFullName { get; set; }
    }
}
