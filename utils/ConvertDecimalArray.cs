using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingLibrary.Utilities.MarketDataFeed;

namespace PricingApp.utils
{
    class ConvertDecimalArray
    {
        public static double[,] listDataFeedToDoubleArray(List<DataFeed> data)
        {
            var underlyingSpots = new double[data.Count, data.First().PriceList.Count];
            int i = 0;
            int j;
            foreach (DataFeed d in data)
            {
                j = 0;
                foreach (decimal val in d.PriceList.Values)
                {
                    underlyingSpots[i, j] = Decimal.ToDouble(val);
                    j++;
                }
                i++;
            }
            return underlyingSpots;
        } 
    }
}
