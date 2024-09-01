using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Database;
using OrderAPI.Models;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class OrderController : ControllerBase
    {
        private readonly ApiDbContext _db;

        public OrderController(ApiDbContext apiDb)
        {
            _db = apiDb;
        }

        [HttpGet("order/list")]    
        public async Task<ActionResult<IEnumerable<ItemOrderModel>>> GetOrders()
        {
            var orders = await _db.Order             
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("order/{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            // Retorna o pedido
            var order = await _db.Order.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new { message = $"Order with ID: {id} not found." });
            }

            // Coleta os produtos do pedido
            var products = await _db.ItemOrder
                .Include(o => o.Product)
                .Where(o => o.OrderId == order.Id)
                .ToListAsync();

            // Monta o obejto padronizado
            var orderResponse = new OrderDto
            {
                Id = order.Id,
                ClientName = order.ClientName,
                ClientEmail = order.ClientEmail,
                Paid = order.Paid,
                TotalValue = (double) products.Sum(o => o.Product.Value * o.Amount), 
                Items = products.Select(product => new ItemOrderDto
                    {
                        Id = product.Order.Id,
                        ProductId = product.Id,
                        ProductName = product.Product.ProductName,
                        UnitValue = (double) product.Product.Value,
                        Amount = product.Amount,
                    }
                ).ToList()
            };

            return Ok(orderResponse);
        }

        [HttpPost("order/add")]
        public async Task<ActionResult<ItemOrderModel>> AddOrder(ItemOrderModel itemOrder)        
        {
            // Verifica se a quantidade é pelo menos 1
            if (itemOrder.Amount < 1)
            {
                return BadRequest(new { message = "The quantity must be equal to or greater than 1!" });
            }
            
            // Verifica se o produto existe
            var existingProduct = await _db.Product.FindAsync(itemOrder.ProductId);
            if (existingProduct == null)
            {
                return NotFound(new { message = $"Product ID: {itemOrder.ProductId} not found!" });
            }

            // Cria o Pedido
            var newOrder = new OrderModel();

            // Verifica se o pedido existe
            var existingOrder = await _db.Order.FindAsync(itemOrder.OrderId);
            if (existingOrder == null)
            {
                try
                {
                    newOrder.ClientName = itemOrder.Order.ClientName;
                    newOrder.ClientEmail = itemOrder.Order.ClientEmail;
                    newOrder.CreationDate = itemOrder.Order.CreationDate;
                    newOrder.Paid = itemOrder.Order.Paid;

                    _db.Order.Add(newOrder);
                    await _db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return BadRequest(new { message = "Error in create order!" });
                }
            } 
            else
            {
                newOrder = existingOrder;
            }

            // Cria o item pedido
            try
            {
                var newItemOrder = new ItemOrderModel
                {
                    Order = newOrder,
                    Product = existingProduct,
                    Amount = itemOrder.Amount
                };

                _db.ItemOrder.Add(newItemOrder);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error in create item order!" });
            }

            return Ok(new { message = "ItemOrder inserted successfully!" });
        }        
    }
}
