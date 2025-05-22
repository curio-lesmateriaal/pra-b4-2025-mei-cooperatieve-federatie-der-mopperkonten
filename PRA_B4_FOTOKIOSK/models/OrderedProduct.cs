using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    public class OrderedProduct
    {
        public string productName { get; set; }
        public double totalPrice { get; set; }
        public int photoID { get; set; }
        public int amount { get; set; }
    }
}
