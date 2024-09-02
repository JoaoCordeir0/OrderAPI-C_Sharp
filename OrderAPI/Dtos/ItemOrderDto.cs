namespace OrderAPI.Dtos
{
    public class ItemOrderDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double UnitValue { get; set; }
        public int Amount { get; set; }
    }
}
