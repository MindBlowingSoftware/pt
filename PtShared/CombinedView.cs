using System;

namespace PtShared
{
    public class CombinedView
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal AmountInvested { get; set; }
        public decimal Value { get; set; }
        public DateTime ValueDate { get; set; }
        public string Type { get; set; }
        public decimal Cmp { get; set; }
        public decimal Qty { get; set; }
    }
}