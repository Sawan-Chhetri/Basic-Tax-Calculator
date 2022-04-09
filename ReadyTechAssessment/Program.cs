using System;
using System.Collections.Generic;

namespace ReadyTechAssessment
{
    class Program
    {
        readonly static string[] exemptedProducts = new string[] { "food", "book", "medicine", "chocolate", "pills" };
        const decimal SalesTaxRate = 0.1m;
        const decimal ImportDutyRate = 0.05m;
        static List<ReceiptData> ShoppedItems = new List<ReceiptData>();
        static decimal totalPrice = 0;
        static decimal totalSalesTax = 0;

        static void Main(string[] args)
        {
            List<Products> CartItems = new List<Products>()
            {
                // Input 1
                new Products() { Quantity = 1, Name = "book", Price = 12.49m},
                new Products() { Quantity = 1, Name = "music cd", Price = 14.99m},
                new Products() { Quantity = 1, Name = "chocolate bar", Price = 0.85m},

                //// Input 2
                //new Products() { Quantity = 1, Name = "imported box of chocolates", Price = 10.00m},
                //new Products() { Quantity = 1, Name = "imported bottle of perfume", Price = 47.50m},

                //// Input 3
                //new Products() { Quantity = 1, Name = "imported bottle of perfume", Price = 27.99m},
                //new Products() { Quantity = 1, Name = "bottle of perfume", Price = 18.99m},
                //new Products() { Quantity = 1, Name = "packet of headache pills", Price = 9.75m},
                //new Products() { Quantity = 1, Name = "box of imported chocolates", Price = 11.25m}
            };

            Products Inputs = new Products();
            ReceiptData receiptData;

            foreach (Products item in CartItems)
            {
                receiptData = new ReceiptData();

                // Get product data.
                Inputs = ProductData(item);

                // Calculate import duty if product is imported.
                if (Inputs.IsImported)
                    receiptData.ImportDuty = Math.Ceiling((Inputs.Price * ImportDutyRate) / 0.05m) * 0.05m;

                // Calculate Sales Tax if not exempted.
                if (!Inputs.SalesTaxExempted)
                {
                    receiptData.SalesTax = Math.Ceiling((Inputs.Price * SalesTaxRate) / 0.05m) * 0.05m;
                }

                // Calculate checkout item price.
                receiptData.CheckOutItemPrice = Inputs.Price + receiptData.ImportDuty + receiptData.SalesTax;

                // Calculate total sales tax.
                totalSalesTax += receiptData.ImportDuty + receiptData.SalesTax;

                // Calculate total price for all items.
                totalPrice += receiptData.CheckOutItemPrice;

                // Name of the product.
                receiptData.Name = Inputs.Name;

                // Quantity of product.
                receiptData.Quantity = Inputs.Quantity;

                // Add all the products to the shopped list.
                ShoppedItems.Add(receiptData);


            }

            // Print the receipt.
            PrintReceipt();
        }

        private static Products ProductData(Products product)
        {
            // Check if the product is exempted from sales tax.
            var exempted = CheckExemption(product.Name, exemptedProducts);
            if (exempted)
            {
                product.SalesTaxExempted = true;
            }

            // Check if product is impoted.
            if (product.Name.Contains("imported"))
            {
                product.IsImported = true;
            }

            return product;
        }

        // Checks if product is exempted from sales tax.
        private static bool CheckExemption(string name, string[] exemptedProducts)
        {
            bool exemption = false;

            for (int i = 0; i < exemptedProducts.Length; i++)
            {
                var index = name.IndexOf(exemptedProducts[i]);

                if (index != -1)
                {
                    exemption = true;
                    break;
                }
                else
                {
                    exemption = false;
                }

            }
            return exemption;
        }

        // Print receipt.
        private static void PrintReceipt()
        {
            foreach (ReceiptData item in ShoppedItems)
            {
                Console.WriteLine("{0}, {1}, {2:0.00}", item.Quantity, item.Name, item.CheckOutItemPrice);
            }
            Console.WriteLine(" ");
            Console.WriteLine("Sales Taxes: " + string.Format("{0:0.00}", totalSalesTax));
            Console.WriteLine("Total: " + totalPrice);

        }
    }
}