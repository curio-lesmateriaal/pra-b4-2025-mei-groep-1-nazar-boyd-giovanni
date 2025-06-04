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
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }


        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();


        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {


            PicturesToDisplay.Clear();

            // Initializeer de lijst met fotos
            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                var now = DateTime.Now;
                int dayToday = (int)now.DayOfWeek;

                string folderName = Path.GetFileName(dir); // bijv. "0_Zondag"
                string[] parts = folderName.Split('_');


                if (parts.Length > 0 && int.TryParse(parts[0], out int folderDay) && folderDay == dayToday)


                    /**
                     *
                     * dir string is de map waar de fotos in staan. Bijvoorbeeld:
                     * \fotos\0_Zondag
                     */
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        /**
                         * file string is de file van de foto. Bijvoorbeeld:
                         * \fotos\0_Zondag\10_05_30_id8824.jpg
                         */
                        {
                            string fileName = Path.GetFileName(file); 
                            string[] fileParts = fileName.Split('_');

                            if (fileParts.Length > 3)
                            {
                                int hour = int.Parse(fileParts[0]);
                                int minute = int.Parse(fileParts[1]);
                                int second = int.Parse(fileParts[2]);

                                DateTime fileDateTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);

                                TimeSpan timeDifference = now - fileDateTime;

                                if (timeDifference.TotalMinutes <= 30 && timeDifference.TotalMinutes >= 2)
                                {
                                    PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                                }
                            }
                        }
                    }
            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {

        }

    }
}
