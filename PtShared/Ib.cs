using System;
using System.Collections.Generic;

namespace PtShared
{
    public partial class Ib
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public int? Quantity { get; set; }
        public string Mult { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? CostBasis { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal? Value { get; set; }
        public decimal? UnrealizedPL { get; set; }
        public string Code { get; set; }
        public DateTime? Dateimported { get; set; }
    }
}
