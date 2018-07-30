using System;

namespace Pt.Controllers
{
    public class PortfolioItem
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal LatestImportedAmount { get; set; }
        public string LatestImportedAmountDate { get; set; }
        public decimal AmountInvested { get; set; }
        public string AmountHistory { get; set; }
        public string AmountRecordedDateHistory { get; set; }
    }
}