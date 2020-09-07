using PricingApp.model;
using PricingLibrary.Computations;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.services.optionPricers
{
    public class BasketOptionPricer : OptionPricer
    {
        public BasketOptionPricer(BasketOption opt) : base(opt)
        {
        }

        public override CompletePricingResults getPricingResults(List<DataFeed> pricingData)
        {
            throw new NotImplementedException();
        }
    }
}
