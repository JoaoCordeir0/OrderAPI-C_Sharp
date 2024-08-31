namespace OrderAPI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Paid { get; set;}        
    }
}
