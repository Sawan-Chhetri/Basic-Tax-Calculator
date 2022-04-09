using System;
namespace ReadyTechAssessment
{
    public class ReceiptData : Products
    {
        public decimal SalesTax { get; set; }

        public decimal ImportDuty { get; set; }

        public decimal FinalItemPrice { get; set; }

        public decimal CheckOutTotalPrice { get; set; }

        public decimal TotalSalesTax { get; set; }

        public decimal TotalTax { get; set; }
    }
}