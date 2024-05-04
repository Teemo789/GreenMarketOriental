using ECommerce.Entities;

namespace ECommerce.Models
{
    public class PromotionModel
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public string Type { get; set; }
        public double MontantReduction { get; set; }

        public Promotion Promotion { get; set; }
    }
}