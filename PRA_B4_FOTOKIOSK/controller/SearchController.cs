using PRA_B4_FOTOKIOSK.magie;
using System;
using System.IO;
using System.Linq;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class SearchController
    {
        public Home Window { get; set; }

        public void Start()
        {
        }

        public void SearchButtonClick()
        {
            string input = SearchManager.GetSearchInput();

            if (string.IsNullOrWhiteSpace(input))
            {
                Window.ShowMessage("Vul een tijd (bijv. 10:10:22) of id in.");
                return;
            }

            string fotosMap = Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\fotos"));

            if (!Directory.Exists(fotosMap))
            {
                Window.ShowMessage("Fotomap niet gevonden.");
                return;
            }

            var alleFotos = Directory.GetFiles(fotosMap, "*.jpg", SearchOption.AllDirectories);

            string inputNormalized = input.Replace("-", "").Replace(":", "").Replace(" ", "").ToLower();

            string gevondenFoto = alleFotos.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f).ToLower().Contains(inputNormalized));

            if (gevondenFoto != null)
            {
                SearchManager.SetPicture(gevondenFoto);

                var naam = Path.GetFileNameWithoutExtension(gevondenFoto); 
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
