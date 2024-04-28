namespace ECommerce.Models
{
    public class AddToCartModel
    {
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }

}
