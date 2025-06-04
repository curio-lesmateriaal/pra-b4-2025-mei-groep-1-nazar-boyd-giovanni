using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        public static Home Window { get; set; }
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        public void Start(bool showMessageBox = false)
        {
            PicturesToDisplay.Clear();

            var now = DateTime.Now;
            int dayToday = (int)now.DayOfWeek;

            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                string folderName = Path.GetFileName(dir);
                if (!folderName.StartsWith(dayToday.ToString())) continue;

                foreach (string file in Directory.GetFiles(dir))
                {
                    string fileName = Path.GetFileName(file);
                    string tijdStr = fileName.Split("_id")[0].Replace("_", ":");
                    DateTime fileDate = DateTime.Parse(tijdStr);

                    if (fileDate >= now.AddMinutes(-30) && fileDate <= now.AddMinutes(-2))
                    {
                        bool toegevoegd = false;

                        for (int i = 0; i < PicturesToDisplay.Count; i++)
                        {
                            string otherName = Path.GetFileName(PicturesToDisplay[i].Source);
                            string otherTijdStr = otherName.Split("_id")[0].Replace("_", ":");
                            DateTime otherDate = DateTime.Parse(otherTijdStr);

                            if (Math.Abs((fileDate - otherDate).TotalSeconds) == 60)
                            {
                                PicturesToDisplay.Insert(i, new KioskPhoto() { Id = 0, Source = file });
                                toegevoegd = true;
                                break;
                            }
                        }

                        if (!toegevoegd)
                        {
                            PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                        }
                    }
                }
            }

            PictureManager.UpdatePictures(PicturesToDisplay);

            if (showMessageBox)
            {
                MessageBox.Show($"Aantal foto's in overzicht: {PicturesToDisplay.Count}", "A3 Check");
            }
        }

        public void RefreshButtonClick()
        {
            Start(true);
        }
    }
}
