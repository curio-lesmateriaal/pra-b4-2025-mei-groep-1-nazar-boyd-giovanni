using System.Windows;
using System.Windows.Controls;
using PRA_B4_FOTOKIOSK.controller;
using PRA_B4_FOTOKIOSK.models;
using System;

namespace PRA_B4_FOTOKIOSK
{
    public partial class Home : Window
    {
        public ShopController ShopController { get; set; }

        public Home()
        {
            InitializeComponent();

            // Controller instellen
            ShopController = new ShopController();
            ShopController.BonUpdated += ToonBon;

            // Prijslijst en producten inladen uit ShopController
            cbProducts.Items.Clear();
            foreach (var product in ShopController.GetProducten())
                cbProducts.Items.Add($"{product.Name} - €{product.Price:0.00} - {product.Description}");

            lbPrices.Content = ShopController.GetPrijslijst();
        }

        private void ToonBon(string tekst)
        {
            tbReceiptTextBlock.Text = tekst;
        }

        private void btnShopAdd_Click(object sender, RoutedEventArgs e)
        {
            string fotoId = tbFotoId.Text.Trim();
            int idx = cbProducts.SelectedIndex;
            string amountText = tbAmount.Text.Trim();

            if (string.IsNullOrWhiteSpace(fotoId) || idx < 0 || string.IsNullOrWhiteSpace(amountText))
            {
                ShowMessage("Vul alle velden in!");
                return;
            }
            if (!int.TryParse(amountText, out int aantal) || aantal < 1)
            {
                ShowMessage("Aantal moet een getal > 0 zijn!");
                return;
            }
            var producten = ShopController.GetProducten();
            KioskProduct? product = idx >= 0 && idx < producten.Count ? producten[idx] : null;
            if (product == null)
            {
                ShowMessage("Onbekend product!");
                return;
            }
            ShopController.AddProductToReceipt(fotoId, product, aantal);
        }

        private void btnShopReset_Click(object sender, RoutedEventArgs e)
        {
            ShopController.ResetReceipt();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string bonText = tbReceiptTextBlock.Text ?? "";
            if (string.IsNullOrWhiteSpace(bonText))
            {
                ShowMessage("Geen bon om op te slaan.");
                return;
            }
            string pad = $"bon_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            System.IO.File.WriteAllText(pad, bonText);
            ShowMessage($"Bon opgeslagen als:\n{pad}");
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message, "Melding", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Andere tabblad-handlers kun je toevoegen hieronder
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // Voor foto's-tab
        }

        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            // Voor zoeken-tab
        }
    }
}
