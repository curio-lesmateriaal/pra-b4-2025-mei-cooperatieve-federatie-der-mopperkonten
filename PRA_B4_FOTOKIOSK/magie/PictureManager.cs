using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace PRA_B4_FOTOKIOSK.magie
{
    public class PictureManager
    {
        public static Home Instance { get; set; }

        public static void UpdatePictures(List<KioskPhoto> PicturesToDisplay)
        {
            Instance.spPictures.Children.Clear();
            WrapPanel wrapPanel = new WrapPanel
            {
                // Green-blue glowing effect
                Margin = new Thickness(10),
                Orientation = Orientation.Horizontal,
                Effect = new DropShadowEffect
                {
                    Color = Color.FromRgb(0, 128, 255), // Blended blue-green color
                    ShadowDepth = 0, // No shadow depth for a glow effect
                    BlurRadius = 10, // Adjust the blur radius for the glowing effect
                    Opacity = 0.8 // Transparency level for the glow
                }


            };

            foreach (KioskPhoto picture in PicturesToDisplay)
            {
                Image image = new Image
                {
                    Source = pathToImage(picture.Source),
                    Width = 1020 / 3.5,
                    Height = 1080 / 3.5,
                    Margin = new Thickness(10),  // Fixed this line
                    Stretch = System.Windows.Media.Stretch.UniformToFill,
                };

                // Adding a border and shadow effect
                Border border = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.Gray),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(5),
                    Child = image
                };

                // Adding mouse hover effect
                border.MouseEnter += (s, e) =>
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220)); // Change background on hover
                };
                border.MouseLeave += (s, e) =>
                {
                    border.Background = Brushes.Transparent; // Reset background
                };

                wrapPanel.Children.Add(border);
                Console.WriteLine(picture);
            }

            Instance.spPictures.Children.Add(wrapPanel);
        }

        public static BitmapImage pathToImage(string path)
        {
            var stream = new MemoryStream(File.ReadAllBytes(path));
            var img = new BitmapImage();

            img.BeginInit();
            img.StreamSource = stream;
            img.EndInit();

            return img;
        }
    }
}

