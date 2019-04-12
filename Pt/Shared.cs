using Pt.Controllers;
using PtShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pt
{
    public class Shared
    {
        public static List<PortfolioItem> GetPortfolioGroupedByCode(List<CombinedView> combinedView, 
            decimal exchangeRate)
        {
            var cv = combinedView
                .GroupBy(a => a.Code)
                .Select(group => new PortfolioItem
                {
                    Id = group.Max(b => b.Id),
                    AmountInvested = Math.Round(group.Max(b => b.AmountInvested) / exchangeRate, 2),
                    Amount = Math.Round(group.OrderByDescending(b => b.ValueDate).First().Value / exchangeRate, 2),
                    Date = group.OrderByDescending(b => b.ValueDate).First().ValueDate,
                    Name = group.Max(b => b.Name),
                    Type = group.Max(b => b.Type),
                    AmountHistory = group.OrderBy(a => a.ValueDate)
                        .Select(b => Math.Round(b.Value / exchangeRate, 2)).ToArray(),
                    AmountHistoryDesc = group.OrderByDescending(a => a.ValueDate)
                        .Select(b => Math.Round(b.Value / exchangeRate, 2)).ToArray(),
                    AmountRecordedDateHistory = group.OrderBy(a => a.ValueDate)
                        .Select(b => b.ValueDate.ToShortDateString()).ToArray(),
                    Code = group.Key,
                    CmpHistory = group.OrderBy(a => a.ValueDate)
                        .Select(b => Math.Round(b.Cmp, 2)).ToArray(),
                    CmpHistoryDesc = group.OrderByDescending(a => a.ValueDate)
                        .Select(b => Math.Round(b.Cmp, 2)).ToArray(),
                    Qty = group.OrderByDescending(b => b.ValueDate).First().Qty

                }).ToList();

            foreach (var cvitem in cv)
            {

                cvitem.Last5DaysPct =
                    Math.Round(((cvitem.AmountHistoryDesc.Take(5).First() -
                    cvitem.AmountHistoryDesc.Take(5).Last()) * 100
                    / cvitem.AmountHistoryDesc.Take(5).Last()), 2);
                cvitem.Last20DaysPct =
                    Math.Round(((cvitem.AmountHistoryDesc.Take(20).First() -
                    cvitem.AmountHistoryDesc.Take(20).Last()) * 100
                    / cvitem.AmountHistoryDesc.Take(20).Last()), 2);
                cvitem.Last100DaysPct =
                    Math.Round(((cvitem.AmountHistoryDesc.Take(100).First() -
                    cvitem.AmountHistoryDesc.Take(100).Last()) * 100
                    / cvitem.AmountHistoryDesc.Take(100).Last()), 2);

                cvitem.MostRecentPct =
                    Math.Round(((cvitem.CmpHistoryDesc.Take(2).First() -
                    cvitem.CmpHistoryDesc.Take(2).Last()) * 100
                    / cvitem.CmpHistoryDesc.Take(2).Last()), 2);
                cvitem.Last5DaysMpPct =
                    Math.Round(((cvitem.CmpHistoryDesc.Take(5).First() -
                    cvitem.CmpHistoryDesc.Take(5).Last()) * 100
                    / cvitem.CmpHistoryDesc.Take(5).Last()), 2);
                cvitem.Last20DaysMpPct =
                    Math.Round(((cvitem.CmpHistoryDesc.Take(20).First() -
                    cvitem.CmpHistoryDesc.Take(20).Last()) * 100
                    / cvitem.CmpHistoryDesc.Take(20).Last()), 2);
                cvitem.Last100DaysMpPct =
                    Math.Round(((cvitem.CmpHistoryDesc.Take(100).First() -
                    cvitem.CmpHistoryDesc.Take(100).Last()) * 100
                    / cvitem.CmpHistoryDesc.Take(100).Last()), 2);
            }

            cv = cv.Where(a => a.Qty > 0).ToList();

            return cv;
        }
        public static PortfolioItem Get(IQueryable<CombinedView> combinedView, decimal exchangeRate)
        {
            

            var portfolio = new PortfolioItem()
            {
                Id = combinedView.Max(b => b.Id),
                AmountInvested = Math.Round(combinedView.Max(b => b.AmountInvested) / exchangeRate, 2),
                Amount = Math.Round(combinedView.OrderByDescending(b => b.ValueDate).First().Value / exchangeRate, 2),
                Date = combinedView.OrderByDescending(b => b.ValueDate).First().ValueDate,
                Name = combinedView.Max(b => b.Name),
                Type = combinedView.Max(b => b.Type),
                AmountHistory = combinedView.OrderByDescending(b => b.ValueDate)
                        //.Take(20)
                        .OrderBy(a => a.ValueDate)
                        .Select(b => Math.Round(b.Value / exchangeRate, 2)).ToArray(),
                AmountRecordedDateHistory = combinedView.OrderByDescending(b => b.ValueDate)
                        //.Take(20)
                        .OrderBy(a => a.ValueDate)
                        .Select(b => b.ValueDate.ToShortDateString()).ToArray(),
                Code = combinedView.Max(b => b.Code),
                CmpHistory = combinedView.OrderByDescending(b => b.ValueDate)
                        //.Take(20)
                        .OrderBy(a => a.ValueDate)
                        .Select(b => Math.Round(b.Cmp, 2)).ToArray(),
                Qty = combinedView.OrderByDescending(b => b.ValueDate).First().Qty
            };

            return portfolio;
        }
    }
}
