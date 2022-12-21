using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STOCKAPI.Models
{
    public class PriceHis
    {
        public double Volume { get; set; }
        public DateTime Time { get; set; }
        public double Openprice { get; set; }
        public double Closeprice { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
    }
}