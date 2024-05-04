using ECommerce.Entities;
using System;

namespace ECommerce.IServices
{
    public interface IReductionCalculationService
    {
        decimal CalculateReductionAmount(decimal reductionPercentage, decimal price);
    }
}
