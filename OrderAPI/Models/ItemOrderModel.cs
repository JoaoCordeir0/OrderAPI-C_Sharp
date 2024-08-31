using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Models
{
    public class ItemOrderModel
    {
        public int Id { get; set; }

        [ForeignKey("OrderId")]
        public OrderModel Order { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
