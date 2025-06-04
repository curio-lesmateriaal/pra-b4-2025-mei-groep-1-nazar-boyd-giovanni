using System;
using System.Windows;
using System.Windows.Controls;
using PRA_B4_FOTOKIOSK.controller;
using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;

namespace PRA_B4_FOTOKIOSK
{
    public partial class Home : Window
    {
        public ShopController ShopController { get; set; }
        public PictureController PictureController { get; set; }
        public SearchController SearchController { get; set; }

        public Home()
        {
            InitializeComponent();

            ShopManager.Instance = this;
            PictureManager.Instance = this;
            SearchManager.Instance = this;

            // ShopController instellen
            ShopController = new ShopController();
            ShopController.BonUpdated += ToonBon;

            // PictureController instellen en meteen starten (geen popup)
            PictureController = new PictureController();
            PictureController.Window = this;
            PictureController.Start(); // Let op: géén true → geen popup

            // Producten en prijslijst uit ShopController laden
            cbProducts.Items.Clear();
            foreach (var product in ShopController.GetProducten())
                cbProducts.Items.Add($"{product.Name} - €{product.Price:0.00} - {product.Description}");

            lbPrices.Content = ShopController.GetPrijslijst();

            // SearchController instellen en koppelen aan dit window
            SearchController = new SearchController();
            SearchController.Window = this;
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

        // Meldingen in een MessageBox tonen
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Melding", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Foto-tab: laadt/refresh de foto's
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            PictureController.Start(true); // nu WEL een popup
        }

        // Koppeling voor de zoeken-knop
        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            SearchController.SearchButtonClick();
        }
    }
}
