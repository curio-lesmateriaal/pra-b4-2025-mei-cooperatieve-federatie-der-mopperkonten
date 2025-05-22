using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PRA_B4_FOTOKIOSK.magie
{
    public class ShopManager
    {

        public static List<KioskProduct> Products = new List<KioskProduct>();    
        public static List<OrderedProduct> receiptProducts = new List<OrderedProduct>();
        public static Home Instance { get; set; }

        public static void SetShopPriceList(string text)
        {
            Instance.lbPrices.Content = text;
        }

        public static void AddShopPriceList(string text)
        {
            Instance.lbPrices.Content = Instance.lbPrices.Content + text;
        }

        public static string GetShopPriceList ()
        {
            return (string)Instance.lbPrices.Content;
        }

        public static void SetShopReceipt(string text)
        {
            Instance.lbReceipt.Content = text;
        }

        public static string GetShopReceipt()
        {
            return (string)Instance.lbReceipt.Content;
        }

        public static void AddShopReceipt(string text)
        {
            SetShopReceipt(GetShopReceipt() + text);
        }

        public static void UpdateDropDownProducts()
        {
            Instance.cbProducts.Items.Clear();
            foreach (KioskProduct item in Products)
            {
                Instance.cbProducts.Items.Add(item.Name);
            }
        }

        public static KioskProduct GetSelectedProduct()
        {
            if (Instance.cbProducts.SelectedItem == null) return null;
            string selected = Instance.cbProducts.SelectedItem.ToString();
            foreach (KioskProduct product in Products)
            {
                if (product.Name == selected) return product;
            }
            return null;
        }

        public static int? GetFotoId()
        {
            int? id = null;
            int amount = -1;
            if (int.TryParse(Instance.tbFotoId.Text, out amount))
            {
                id = amount;
            }
            return id;
        }

        public static int? GetAmount()
        {
            int? id = null;
            int amount = -1;
            if (int.TryParse(Instance.tbAmount.Text, out amount))
            {
                id = amount;
            }
            return id;
        }

        public static double GetTotalPrice()
        {
            double total = 0;

            foreach (var item in receiptProducts)
            {
                total += item.totalPrice;
            }

            return total;
        }
    }
}
