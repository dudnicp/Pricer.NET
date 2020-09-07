using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using PricingLibrary.Utilities.MarketDataFeed;

namespace PricingApp.services
{
    class StatComputing
    {
       public double[,] correlationMatrix(DataFeed data)
        {
            var underlyingSpots = data.PriceList.Values;
            return new double[0, 0];
        }
    }
}
