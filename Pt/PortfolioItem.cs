using System;

namespace Pt.Controllers
{
    public class PortfolioItem
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal LatestImportedAmount { get; set; }
        public string LatestImportedAmountDate { get; set; }
        public decimal AmountInvested { get; set; }
        public decimal[] AmountHistory { get; set; }
        public decimal[] AmountHistoryDesc { get; set; }
        public string[] AmountRecordedDateHistory { get; set; }
        public string Code { get; set; }
        public decimal[] CmpHistory { get; set; }
        public decimal[] CmpHistoryDesc { get; set; }
        public decimal[] TypeHistory { get; set; }
        public decimal MostRecentPct { get; set; }
        public decimal Last5DaysPct { get; set; }
        public decimal Last20DaysPct { get; set; }
        public decimal Last100DaysPct { get; set; }
        public decimal Last5DaysMpPct { get; set; }
        public decimal Last20DaysMpPct { get; set; }
        public decimal Last100DaysMpPct { get; set; }
        public decimal Qty { get; set; }
    }
}