public class Order
{
    public int Id { get; set; }
    public string ItemName { get; set; }    
    public int Quantity { get; set; }
    public DateTime DeliveryTime { get; set; }
    public string DeliveryAddress { get; set; }
    public string ContactPhone { get; set; }
}

public class OrderContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
}

public class OrderController : ApiController
{
    private OrderContext db = new OrderContext();

    [HttpPost]
    public IHttpActionResult PlaceOrder(Order order)
    {
        db.Orders.Add(order);
        db.SaveChanges();
        return Ok("Order placed successfully");
    }

    [HttpPut]
    public IHttpActionResult UpdateOrder(int id, Order updatedOrder)
    {
        var existingOrder = db.Orders.Find(id);
        if (existingOrder != null)
        {
            existingOrder.DeliveryTime = updatedOrder.DeliveryTime;
            existingOrder.DeliveryAddress = updatedOrder.DeliveryAddress;
            db.SaveChanges();
            return Ok("Order updated successfully");
        }
        return NotFound();
    }
}
