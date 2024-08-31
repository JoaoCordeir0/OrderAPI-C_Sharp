namespace OrderAPI.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public bool Paid { get; set; }
        public double TotalValue { get; set; }
        public List<ItemsOrderDto> ItemsOrder { get; set; }
    }
}
