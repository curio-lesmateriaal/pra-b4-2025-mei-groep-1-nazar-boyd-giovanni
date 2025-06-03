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
            // 1. Haal de zoekinput op via de magie (bijv. "10:10:22" of "id1111")
            string input = SearchManager.GetSearchInput();

            if (string.IsNullOrWhiteSpace(input))
            {
                Window.ShowMessage("Vul een tijd (bijv. 10:10:22) of id in.");
                return;
            }

            // 2. Zoek door alle foto's heen (bijvoorbeeld in "fotos"-map onder je projectdirectory)
            string fotosMap = Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\fotos"));

            if (!Directory.Exists(fotosMap))
            {
                Window.ShowMessage("Fotomap niet gevonden.");
                return;
            }

            var alleFotos = Directory.GetFiles(fotosMap, "*.jpg", SearchOption.AllDirectories);

            // Normaliseer input voor zoeken (maak het vergelijkbaar met bestandsnaam)
            string inputNormalized = input.Replace("-", "").Replace(":", "").Replace(" ", "").ToLower();

            string gevondenFoto = alleFotos.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f).ToLower().Contains(inputNormalized));

            if (gevondenFoto != null)
            {
                // Toon de foto in de UI
                SearchManager.SetPicture(gevondenFoto);

                // Parse dag, tijd en id netjes uit de bestandsnaam en map
                var naam = Path.GetFileNameWithoutExtension(gevondenFoto); // bijv. "10_10_22_id1111"
                var delen = naam.Split('_');
                string tijd = "";
                string id = "";
                if (delen.Length >= 4)
                {
                    tijd = $"{delen[0]}:{delen[1]}:{delen[2]}";
                    id = delen[3];
                }
                string mapnaam = Path.GetFileName(Path.GetDirectoryName(gevondenFoto));
                string info = $"Dag: {mapnaam}\nTijd: {tijd}\nId: {id}\nBestand: {Path.GetFileName(gevondenFoto)}";
                SearchManager.SetSearchImageInfo(info);
            }
            else
            {
                Window.ShowMessage("Geen foto gevonden met deze zoekopdracht.");
            }
        }
    }
}
