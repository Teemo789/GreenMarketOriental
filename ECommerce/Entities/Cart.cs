namespace ECommerce.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public ICollection<CartLine> CartLines { get; set; }

        public void AddItem(Product product, int quantity)
        {
            var line = CartLines.FirstOrDefault(p => p.Product.Id == product.Id);

            if (line == null)
            {
                line = new CartLine
                {
                    Product = product,
                    Quantity = quantity
                };
                CartLines.Add(line);
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product, int quantity)
        {
            var line = CartLines.FirstOrDefault(p => p.Product.Id == product.Id);

            if (line != null)
            {
                if (line.Quantity > quantity)
                {
                    line.Quantity -= quantity;
                }
                else
                {
                    CartLines.Remove(line);
                }
            }
        }
        public decimal ComputeTotalValue()
        {
            return CartLines.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            CartLines.Clear();
        }
    }
}
