    namespace ECommerce.Entities
    {
        public class Card : Payment
        {
            public string CardNumber { get; set; }
            public int ExpirationMonth { get; set; }
            public int ExpirationYear { get; set; }
            public string CardHolderName { get; set; }
        }

    }
