using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccessContext;
using PricingLibrary.FinancialProducts;

namespace PricingApp.services
{
    public class DataBaseServices
    {
        public static List<Share> getShares()
        {
            List<Share> allShares = new List<Share>();
            using (DataBaseAccessDataContext asdc = new DataBaseAccessDataContext())
            {
                var allSharesQuery = (from share in asdc.ShareNames select share).Distinct();
                foreach (var share in allSharesQuery)
                {
                    Share newShare = new Share(share.name, share.id);
                    allShares.Add(newShare);
                }
            }
            return allShares;
        }

        public static Dictionary<DateTime, decimal> getShareValues(string shareID)
        {
            var values = new Dictionary<DateTime, decimal>();
            using (DataBaseAccessDataContext asdc = new DataBaseAccessDataContext())
            {
                var shareQuery = (from share in asdc.HistoricalShareValues where share.id == shareID orderby share.date select share);
                foreach (var share in shareQuery)
                {
                    values.Add(share.date, share.value);
                }
            }
            return values;
        }
    }
}
