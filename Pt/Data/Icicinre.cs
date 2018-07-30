using System;
using System.Collections.Generic;

namespace Pt.Data
{
    public partial class Icicinre
    {
        public string StockSymbol { get; set; }
        public string CompanyName { get; set; }
        public string IsinCode { get; set; }
        public string Qty { get; set; }
        public string AverageCostPrice { get; set; }
        public string CurrentMarketPrice { get; set; }
        public string ChangeOverPrevClose { get; set; }
        public string ValueAtCost { get; set; }
        public string ValueAtMarketPrice { get; set; }
        public string RealizedProfitLoss { get; set; }
        public string UnrealizedProfitLoss { get; set; }
        public string UnrealizedProfitLoss1 { get; set; }
        public string Column12 { get; set; }
        public DateTime? Dateimported { get; set; }
        public long Id { get; set; }
    }
}
