using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBCP.Models
{
    public class ChangeCurrencyArgs
    {
        public bool ChangeInput { get; set; }
        public CurrencyWrapper CurrencyWrapper { get; set; }
    }
}
