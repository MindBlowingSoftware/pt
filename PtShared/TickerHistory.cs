using System;
using System.Collections.Generic;

namespace PtShared
{
    public partial class TickerHistory
    {
        public DateTime? Daterecorded { get; set; }
        public decimal? Value { get; set; }
        public string SchemeCode { get; set; }
        public long Id { get; set; }
    }
}
