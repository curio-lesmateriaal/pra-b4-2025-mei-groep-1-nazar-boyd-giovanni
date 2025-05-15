using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {
        private decimal totalAmount = 0m;
        public static Home Window { get; set; }

        private Dictionary<string, decimal> productPrices = new Dictionary<string, decimal>
        {
            {"Foto 10x15", 2.55m}
        };
        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen:\nFoto 10x15: €2.55");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15" });

            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();
        }

        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            KioskProduct selectedProduct = ShopManager.GetSelectedProduct();
            int? fotoId = ShopManager.GetFotoId();
            int? amount = ShopManager.GetAmount();

            // Fix: Use the Name property of KioskProduct to get the string key for the dictionary
            decimal price = productPrices[selectedProduct.Name];
            decimal subtotal = (decimal)(price * amount);
            totalAmount += subtotal;

            // Voeg toe aan bon
            string receiptLine = $"\nFoto {fotoId}: {selectedProduct.Name} x{amount} à €{price} = €{subtotal}";
            ShopManager.AddShopReceipt(receiptLine);

            // Update totaalbedrag
            UpdateTotal();
        }
        

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {
            totalAmount = 0m;
            ShopManager.SetShopReceipt("Eindbedrag:");
        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
            string receipt = ShopManager.GetShopReceipt();
        }

        public void UpdateTotal()
        {
            string receipt = ShopManager.GetShopReceipt();
            int lastLineIndex = receipt.LastIndexOf('\n');
            if (lastLineIndex >= 0)
            {
                receipt = receipt.Substring(0, lastLineIndex);
            }
            ShopManager.SetShopReceipt($"{receipt}\nTotaal: €{totalAmount.ToString("0.00")}");
        }
    }
}