using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Entities
{

        public class Promotion
        {
        [Key]
        public string IdPromotion { get; set; }

        // Autres propriétés de la promotion
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Type { get; set; }
        public decimal MontantReduction { get; set; }
        
        // Relation avec ProductInPromotion
        public List<ProductInPromotion> Products { get; set; }
    }

    public class ProductInPromotion
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // Clé étrangère vers Products
        public decimal ReductionAmount { get; set; }
        public Promotion Promotion { get; set; }
        public Product Product { get; set; }
    }

}