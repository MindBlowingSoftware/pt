﻿using System;

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
        public DateTime[] AmountRecordedDateHistory { get; set; }
    }
}