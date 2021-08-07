using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBCP.Models
{
    public class Currency
    {
        public string Name { get; set; }
        public string NameAbb { get; set; }
        public double BuyingRateDollar { get; set; }
        public double SellingRateDollar { get; set; }
        public string FlagName { get; set; }
        public string ImageFlagRoute { get; set; }
    }
}
