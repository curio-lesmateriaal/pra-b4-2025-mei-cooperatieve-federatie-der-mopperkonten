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
            // Haal het huidige dagnummer op (0 = Zondag t/m 6 = Zaterdag)
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            // Initializeer de lijst met fotos
            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                // Verkrijg het eerste deel van de mapnaam (bijv. "2" van "2_Dinsdag")
                string dirName = Path.GetFileName(dir);
                int dirDayNumber;

                // Probeer de mapnaam te converteren naar een integer
                if (int.TryParse(dirName.Split('_')[0], out dirDayNumber))
                {
                    // Controleer of de map overeenkomt met het huidige dagnummer
                    if (dirDayNumber == day)
                    {
                        foreach (string file in Directory.GetFiles(dir))
                        {
                            // Voeg de foto toe aan de lijst
                            PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
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
            // Hier kan je de logica voor de refresh-knop implementeren
        }
    }
}
