namespace OrderAPI.Models
{
    public class AddOrderModel
    {
        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public bool Paid { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
