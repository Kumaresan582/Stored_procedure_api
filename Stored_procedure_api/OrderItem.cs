namespace Stored_procedure_api
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderItem(string productName, int quantity, decimal price)
        {
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }

    public class CreateOrderViewModel
    {
        public string CustomerName { get; set; }
        public int OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

}
