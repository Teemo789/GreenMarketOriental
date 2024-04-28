namespace ECommerce.Entities
{
    public class CartLine
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public decimal TotalPrice { get { return Product.Price * Quantity; } }

        public virtual Product Product { get; set; }
    }

}
