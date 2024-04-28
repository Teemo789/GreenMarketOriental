namespace ECommerce.Entities
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}