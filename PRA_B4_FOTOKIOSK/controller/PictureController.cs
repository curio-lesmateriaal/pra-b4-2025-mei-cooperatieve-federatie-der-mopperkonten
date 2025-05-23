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
        public static Home Window { get; set; }
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        public void Start()
        {
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;


            //check of het tijd boven 10:05:30 is zo niet geef melding dat ze pas dan beschikbaar zijn

            TimeSpan availableStartTime = new TimeSpan(10, 5, 30);
            TimeSpan availableEndTime = new TimeSpan(10, 5, 31);

            if (now.TimeOfDay < availableStartTime)
            {
                Console.WriteLine("De foto's zijn beschikbaar vanaf 10:05 uur");
                return;
            }
            else if (now.TimeOfDay > availableEndTime)
            {
                Console.WriteLine("Welkom!");
            }

            //---------------------------------------------------------------------------------------

            //Kijkt de tijd nu en geeft alles tussen 2min en 30min
            DateTime thirtyMinutesAgo = now.AddMinutes(-30);
            DateTime twoMinutesAgo = now.AddMinutes(-2);

            var photosWithDateTime = new List<(KioskPhoto Photo, DateTime DateTime)>();

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
                            string fileName = Path.GetFileNameWithoutExtension(file);
                            string[] parts = fileName.Split('_');

                            if (parts.Length >= 3)
                            {
                                int hour = int.Parse(parts[0]);
                                int minute = int.Parse(parts[1]);
                                int second = int.Parse(parts[2]);

                                DateTime fileDateTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);
                                Trace.WriteLine(fileDateTime);

                                if (fileDateTime < twoMinutesAgo && fileDateTime > thirtyMinutesAgo)
                                {
                                    photosWithDateTime.Add((new KioskPhoto() { Id = 0, Source = file }, fileDateTime));
                                }
                            }
                        }
                    }
                }
            }

            PicturesToDisplay = photosWithDateTime
                .OrderByDescending(p => p.DateTime).Select(p => p.Photo).ToList();

            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        public void RefreshButtonClick()
        {
        }
    }
}
