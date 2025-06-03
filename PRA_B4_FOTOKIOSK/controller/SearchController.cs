using PRA_B4_FOTOKIOSK.magie;
using System;
using System.IO;
using System.Linq;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class SearchController
    {
        // Statische referentie naar je hoofdscherm
        public Home Window { get; set; }

        // Start-methode, kan leeg blijven
        public void Start()
        {
        }

        // Wordt uitgevoerd wanneer er op de Zoeken knop is geklikt
        public void SearchButtonClick()
        {
            // 1. Haal de zoekinput op via de magie (bijv. "2024-06-04 15:23")
            string input = SearchManager.GetSearchInput();

            if (string.IsNullOrWhiteSpace(input))
            {
                Window.ShowMessage("Vul een dag en/of tijd in.");
                return;
            }

            // 2. Zoek door alle foto's heen (bijvoorbeeld in "Fotos"-map onder je projectdirectory)
            string fotosMap = Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\fotos"));


            if (!Directory.Exists(fotosMap))
            {
                Window.ShowMessage("Fotomap niet gevonden.");
                return;
            }

            var alleFotos = Directory.GetFiles(fotosMap, "*.jpg", SearchOption.AllDirectories);

            // Normaliseer input voor zoeken (maak het vergelijkbaar met bestandsnaam)
            string inputNormalized = input.Replace("-", "").Replace(":", "").Replace(" ", "");
            string gevondenFoto = alleFotos.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f).Contains(inputNormalized));

            if (gevondenFoto != null)
            {
                // Toon de foto in de UI
                SearchManager.SetPicture(gevondenFoto);

                // Toon extra info (bijvoorbeeld bestandsnaam, tijdstip, etc.)
                SearchManager.SetSearchImageInfo(
                    $"Bestand: {Path.GetFileName(gevondenFoto)}\nPad: {gevondenFoto}");
            }
            else
            {
                Window.ShowMessage("Geen foto gevonden met deze dag/tijd.");
            }
        }
    }
}
