using System;
using System.Collections.Generic;

namespace Pt.Data
{
    public partial class YesBankWm
    {
        public string Units { get; set; }
        public string Nav { get; set; }
        public string HniCd { get; set; }
        public string HniNm { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string SchemeCode { get; set; }
        public string SchemeName { get; set; }
        public string AmountInvested { get; set; }
        public string WeightNav { get; set; }
        public string CurrentValue { get; set; }
        public string PerTotal { get; set; }
        public string Dividend { get; set; }
        public string RealisedGainLoss { get; set; }
        public string UnrealisedGainLoss { get; set; }
        public string IrrPercentage { get; set; }
        public string HniProdIrr { get; set; }
        public string HniCatIrr { get; set; }
        public string ProdDescCd { get; set; }
        public string ProdDesc { get; set; }
        public string ValDate { get; set; }
        public long Id { get; set; }
    }
}
