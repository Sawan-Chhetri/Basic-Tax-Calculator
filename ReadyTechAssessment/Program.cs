using System;
using System.Collections.Generic;

namespace ReadyTechAssessment
{
    class Program
    {
        readonly static string[] exemptedProducts = new string[] { "food", "book", "medicine", "chocolate", "pills" };
        const decimal SalesTaxRate = 0.1m;
        const decimal ImportDutyRate = 0.05m;
        static List<ReceiptData> ShoppedItems;
        static decimal totalPrice = 0;
        static decimal totalSalesTax = 0;

        static void Main(string[] args)
        {
            List<List<Products>> AllInputs = new List<List<Products>>();

            List<Products> Input_1 = new List<Products>()
            {
                // Input 1
                new Products() { Quantity = 1, Product = "book", Price = 12.49m},
                new Products() { Quantity = 1, Product = "music cd", Price = 14.99m},
                new Products() { Quantity = 1, Product = "chocolate bar", Price = 0.85m}
            };
            List<Products> Input_2 = new List<Products>()
            {
                // Input 2
                new Products() { Quantity = 1, Product = "imported box of chocolates", Price = 10.00m},
                new Products() { Quantity = 1, Product = "imported bottle of perfume", Price = 47.50m}
            };
            List<Products> Input_3 = new List<Products>()
            {
                // Input 3
                new Products() { Quantity = 1, Product = "imported bottle of perfume", Price = 27.99m},
                new Products() { Quantity = 1, Product = "bottle of perfume", Price = 18.99m},
                new Products() { Quantity = 1, Product = "packet of headache pills", Price = 9.75m},
                new Products() { Quantity = 1, Product = "box of imported chocolates", Price = 11.25m}
            };

            // Add all inputs to AllInputs list.
            AllInputs.Add(Input_1);
            AllInputs.Add(Input_2);
            AllInputs.Add(Input_3);

            Products productData = new Products();
            ReceiptData receiptData;

            // Iterate the 3 set of inputs. 
            foreach (var input in AllInputs)
            {
                // Reset these variables after every set of input finishes.
                ShoppedItems = new List<ReceiptData>();
                totalPrice = 0;
                totalSalesTax = 0;

                // Iterate each item in the particular input.
                foreach (Products item in input)
                {
                    receiptData = new ReceiptData();

                    // Get product data.
                    productData = CheckProduct(item);

                    // Calculate import duty if product is imported.
                    if (productData.IsImported)
                        receiptData.ImportDuty = Math.Ceiling((productData.Price * ImportDutyRate) / 0.05m) * 0.05m;

                    // Calculate Sales Tax if not exempted.
                    if (!productData.SalesTaxExempted)
                    {
                        receiptData.SalesTax = Math.Ceiling((productData.Price * SalesTaxRate) / 0.05m) * 0.05m;
                    }

                    // Calculate final price of the item.
                    receiptData.FinalItemPrice = item.Quantity * (item.Price + receiptData.ImportDuty + receiptData.SalesTax);

                    // Calculate total sales tax.
                    totalSalesTax += item.Quantity * (receiptData.ImportDuty + receiptData.SalesTax);

                    // Calculate total price for all items.
                    totalPrice += receiptData.FinalItemPrice;

                    // Product of the product.
                    receiptData.Product = item.Product;

                    // Quantity of product.
                    receiptData.Quantity = item.Quantity;

                    // Add all the products to the shopped list.
                    ShoppedItems.Add(receiptData);
                }

                // Print the receipt.
                PrintReceipt();
            }
        }

        private static Products CheckProduct(Products product)
        {
            // Check if the product is exempted from sales tax.
            var exempted = CheckExemption(product.Product, exemptedProducts);
            if (exempted)
            {
                product.SalesTaxExempted = true;
            }

            // Check if product is impoted.
            if (product.Product.Contains("imported"))
            {
                product.IsImported = true;
            }

            return product;
        }

        // Checks if product is exempted from sales tax.
        private static bool CheckExemption(string Product, string[] exemptedProducts)
        {
            bool exemption = false;

            for (int i = 0; i < exemptedProducts.Length; i++)
            {
                var index = Product.IndexOf(exemptedProducts[i]);

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
                Console.WriteLine("{0}, {1}, {2:0.00}", item.Quantity, item.Product, item.FinalItemPrice);
            }
            Console.WriteLine(" ");
            Console.WriteLine("Sales Taxes: " + string.Format("{0:0.00}", totalSalesTax));
            Console.WriteLine("Total: " + totalPrice);
            Console.WriteLine(" ");
        }
    }
}