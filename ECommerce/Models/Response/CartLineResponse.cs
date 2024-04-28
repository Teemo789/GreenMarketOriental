namespace ECommerce.Models.Response
{
    public class CartLineResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public decimal TotalPrice { get; set; }

        public ProductResponse Product { get; set; }
    }
}
