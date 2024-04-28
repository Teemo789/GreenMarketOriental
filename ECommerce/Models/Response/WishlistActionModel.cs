namespace ECommerce.Models
{
    public class WishlistActionModel
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public bool AddToWishlist { get; set; } // Indique si l'action est d'ajouter à la wishlist (true) ou de retirer de la wishlist (false)
    }
}
