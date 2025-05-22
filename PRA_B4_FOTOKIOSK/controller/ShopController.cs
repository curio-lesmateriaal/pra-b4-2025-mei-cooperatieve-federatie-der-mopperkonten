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
    public class ShopController
    {

        public static Home Window { get; set; }

        public void Start()
        {
            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15", Price = "2,55", Description = "10x15 formaat" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x20", Price = "2,95", Description = "10x18 formaat" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x25", Price = "3,65", Description = "10x25 formaat" });

            // Stel de prijslijst in aan de rechter kant.
            foreach (KioskProduct product in ShopManager.Products)
            {
                ShopManager.AddShopPriceList($"{product.Name}: €{product.Price}\n");
                ShopManager.GetShopPriceList();
            }

            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();
        }

        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            int? photoCount = ShopManager.GetAmount();
            int? photoId = ShopManager.GetFotoId();
            KioskProduct product = ShopManager.GetSelectedProduct();

            if (photoCount.HasValue && product != null && photoId.HasValue) { // make sure everything is valid
                double productPrice = 0;

                foreach (var item in ShopManager.Products)
                {
                    if (item.Name == product.Name)
                    {
                        productPrice = double.Parse(item.Price);
                        break;
                    }
                }

                ShopManager.SetShopReceipt("Eindbedrag\n€" + (photoCount * productPrice).ToString());
            }
        }

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {

        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
            int receiptCount = 0;
            receiptCount++;
            
            string receipt = $"Product:        {ShopManager.GetSelectedProduct().Name}\n" +
                                      $"Beschrijving:   {ShopManager.GetSelectedProduct().Description}\n" +
                                      $"Aantal:         {ShopManager.GetAmount().ToString()}\n\n" +
                                      $"Prijs:          €{ShopManager.GetSelectedProduct().Price}";


            string receiptsPath = "../../../receipts";
            if (!Directory.Exists(receiptsPath))
            {
                Directory.CreateDirectory(receiptsPath);
            }
            int fileCount = Directory.GetFiles(receiptsPath).Length;
            File.WriteAllText($"{receiptsPath}/receipt{fileCount}.txt", receipt);
        }
    }
}
