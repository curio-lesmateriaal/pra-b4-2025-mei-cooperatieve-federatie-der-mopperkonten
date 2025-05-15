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
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen:\nFoto 10x15: €2.55");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15" });
            
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
                // KioskProduct doesnt have a price member so will calculate manually for now, and no function for getting price yet
                if (product.Name == "Foto 10x15")
                {
                    productPrice = 2.25;
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
        }

    }
}
