using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {
        private List<OrderedProduct> bestellingen = new List<OrderedProduct>();
        private List<KioskProduct> producten = new List<KioskProduct>();

        public event Action<string>? BonUpdated;

        public ShopController()
        {
            producten.Add(new KioskProduct { Name = "Foto 10x15", Price = 2.55m, Description = "Kleine standaard foto" });
            producten.Add(new KioskProduct { Name = "Foto 20x30", Price = 4.95m, Description = "Grote afdruk op glanzend papier" });
            producten.Add(new KioskProduct { Name = "Foto op canvas", Price = 9.95m, Description = "Luxe canvas print" });
        }

        public List<KioskProduct> GetProducten() => producten;

        public string GetPrijslijst()
        {
            var sb = new StringBuilder();
            foreach (var p in producten)
                sb.AppendLine($"{p.Name}: €{p.Price:0.00} - {p.Description}");
            return sb.ToString();
        }

        public void AddProductToReceipt(string fotoId, KioskProduct product, int aantal)
        {
            var bestelling = new OrderedProduct
            {
                FotoId = fotoId,
                ProductNaam = product.Name,
                Aantal = aantal,
                TotaalPrijs = product.Price * aantal
            };
            bestellingen.Add(bestelling);
            UpdateBon();
        }

        public void ResetReceipt()
        {
            bestellingen.Clear();
            UpdateBon();
        }

        private void UpdateBon()
        {
            decimal totaal = 0;
            var sb = new StringBuilder();
            foreach (var b in bestellingen)
            {
                sb.AppendLine(b.ToString());
                totaal += b.TotaalPrijs;
            }
            sb.AppendLine($"\nEindbedrag:\n€{totaal:0.00}");
            BonUpdated?.Invoke(sb.ToString());
        }
    }
}
