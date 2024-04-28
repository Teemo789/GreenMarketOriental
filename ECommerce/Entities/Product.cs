namespace ECommerce.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<CartLine> CartLines { get; set; } = new List<CartLine>();
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }

}
