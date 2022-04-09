using System;
namespace ReadyTechAssessment
{
    public class Products
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsImported { get; set; }

        public bool SalesTaxExempted { get; set; }

        public int Quantity { get; set; }
    }
}