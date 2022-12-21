using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STOCKAPI.Models
{
    public class STOCK
    {
        public STOCK() { PriceHis = new List<PriceHis>(); }
        public string Mack { get; set; }
        public List<PriceHis> PriceHis { get; set; }
    }
}