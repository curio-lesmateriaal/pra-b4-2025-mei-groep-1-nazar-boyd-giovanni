using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {
        private decimal totalAmount = 0m;
        public static Home Window { get; set; }

        public void Start()
        {
            // Maak een lijst van producten aan
            var producten = new List<KioskProduct>
            {
                new KioskProduct { Name = "Foto 10x15", Price = 2.55m, Description = "Kleine standaard foto" },
                new KioskProduct { Name = "Foto 20x30", Price = 4.95m, Description = "Grote afdruk op glanzend papier" },
                new KioskProduct { Name = "Foto op canvas", Price = 9.95m, Description = "Luxe canvas print" }
            };

            // Voeg producten toe aan de ShopManager lijst
            foreach (var product in producten)
            {
                ShopManager.Products.Add(product);
            }

            // Genereer de prijslijst
            ShopManager.SetShopPriceList("Prijzen:");
            foreach (KioskProduct product in ShopManager.Products)
            {
                string lijn = $"{product.Name} - {product.Description}: €{product.Price:F2}";
                ShopManager.AddShopPriceList(lijn);
            }

            // Update de dropdown
            ShopManager.UpdateDropDownProducts();

            // Initieer de bon
            ShopManager.SetShopReceipt("Eindbedrag:\n€0.00");
        }

        // De rest (AddButtonClick etc.) hoort bij C1 en hoef je niet te doen voor B1.
        public void AddButtonClick()
        {

        }

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {

        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
        }
    }
}
