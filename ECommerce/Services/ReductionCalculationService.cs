using ECommerce.Entities;
using ECommerce.IServices;
using System;

namespace ECommerce.Services
{
    public class ReductionCalculationService : IReductionCalculationService
    {
        public decimal CalculateReductionAmount(decimal reductionPercentage, decimal price)
        {
            // Calcul du montant de réduction en pourcentage du prix initial
            decimal reductionAmount = (reductionPercentage / 100) * price;

            return reductionAmount;
        }
    }
}
