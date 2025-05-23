using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            // Calculate the time range
            DateTime thirtyMinutesAgo = now.AddMinutes(-30); // 30 minutes ago
            DateTime twoMinutesAgo = now.AddMinutes(-2);  // 2 minutes ago

            // Temporary list to hold photos with their DateTime
            var photosWithDateTime = new List<(KioskPhoto Photo, DateTime DateTime)>();

            // Initialize the list with photos
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                string dirName = Path.GetFileName(dir);
                int dirDayNumber;

                if (int.TryParse(dirName.Split('_')[0], out dirDayNumber))
                {
                    if (dirDayNumber == day)
                    {
                        foreach (string file in Directory.GetFiles(dir))
                        {
                            // Retrieve the file name without extension
                            string fileName = Path.GetFileNameWithoutExtension(file);
                            string[] parts = fileName.Split('_');

                            // Ensure the file name has the correct format
                            if (parts.Length >= 3) // HH_MM_SS_idxxxx
                            {
                                // Parse the time components
                                int hour = int.Parse(parts[0]);
                                int minute = int.Parse(parts[1]);
                                int second = int.Parse(parts[2]);

                                // Create DateTime from file name
                                DateTime fileDateTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);
                                Trace.WriteLine(fileDateTime);

                                // Check if the photo is within the specified range
                                if (fileDateTime < twoMinutesAgo && fileDateTime > thirtyMinutesAgo)
                                {
                                    // Add to the temporary list if within bounds
                                    photosWithDateTime.Add((new KioskPhoto() { Id = 0, Source = file }, fileDateTime));
                                }
                            }
                        }
                    }
                }
            }

            // Sort the photos by DateTime in descending order
            PicturesToDisplay = photosWithDateTime
                .OrderByDescending(p => p.DateTime).Select(p => p.Photo).ToList();

            // Update the photos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }



        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            // Hier kan je de logica voor de refresh-knop implementeren
        }
    }
}
